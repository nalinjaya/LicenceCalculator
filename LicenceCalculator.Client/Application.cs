using Autofac;
using LicenceCalculator.Domain;
using LicenceCalculator.Repository;
using Serilog;
using System;
using System.IO;

namespace LicenceCalculator.Client
{
    public class Application
    {
        public readonly IUserInstallationRequirementRepository _repo;
        public readonly ILicenceCalculationService _licenceCalculationService;
        private static IContainer _container;
        // Manually instantiate logger so that it can be used before container instantiation.
        private static readonly ILogger _logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        private readonly string defaultPath = @"C:\LicenceCalculator\sample-large.csv";
        private readonly int defaultApplicationId = 374;

        public Application(IUserInstallationRequirementRepository repo, ILicenceCalculationService licenceCalculationService)
        {
            _repo = repo;
            _licenceCalculationService = licenceCalculationService;
        }
        public static Application GetInstance()
        {
            if (_container == null) BuildContainer();
            return _container.Resolve<Application>();
        }

        public void Run()
        {
            var fileInfo = GetValidFileInfo();
            LogFileSize(fileInfo);
            var applicationId = GetValidApplicationId();

            try
            {
                _logger.Information("Reading file. {file} for application Id {applicationId}", fileInfo.FullName, applicationId);
                var usereInstallationRequirements = _repo.GetByApplicationId(fileInfo.FullName, applicationId);
                _logger.Information ("Calculating licences for {application}.", applicationId);
                var nuberOfLicencesRequired = _licenceCalculationService.CalculateRequiredNumberOfLicences(usereInstallationRequirements);
                _logger.Information("Calculation complete. Result:{result}", nuberOfLicencesRequired);
                WriteLine("You need {0} licences", nuberOfLicencesRequired);
                Console.Read();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
            }
        }

        #region Private Helpers

        private string GetFilePathForTheFirstTime()
        {
            WriteLine("Please enter the file path (Press Enter for default:{0})", defaultPath);
            var input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? defaultPath : input;
        }
        private FileInfo GetValidFileInfo()
        {
            var filePath = GetFilePathForTheFirstTime();
            var fileInfo = new FileInfo(filePath);

            while (!fileInfo.Exists)
            {
                _logger.Error("Invalid file path. {path}", fileInfo.FullName);
                WriteLine("Please enter a valid file path.");
                filePath = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(filePath)) continue;
                fileInfo = new FileInfo(filePath);
            }
            return fileInfo;
        }

        private void WriteLine (string format, params object[] args)
        {
            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(format, args);
            Console.ForegroundColor = foregroundColor;
        }

        private int GetValidApplicationId()
        {
            string input = "";
            int aplicationId = 0;
            while (!int.TryParse(input, out aplicationId))
            {
                WriteLine("For what application ID would you like to calculate licences count: (Press Enter for default:{0})", defaultApplicationId);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) { return defaultApplicationId; }
            }
            return aplicationId;
        }

        private void LogFileSize(FileInfo fileInfo)
        {
            var fileSize = fileInfo.Length / (1024 * 1024);
            if (fileSize > 500)
                _logger.Warning("This file seem to be quite large ({fileSize} Mb). Calculation may take some time.", fileSize);
        }

        private static void BuildContainer()
        {
            _logger.Information("Building container.");
            var builder = new ContainerBuilder();
            builder.RegisterType<UserInstallationRequirementRepository>().As<IUserInstallationRequirementRepository>();
            builder.RegisterType<LicenceCalculationService>().As<ILicenceCalculationService>();
            builder.RegisterType<Application>().As<Application>().SingleInstance();

            _container = builder.Build();
            _logger.Information("Building container completed.");
        }

        #endregion
    }
}
