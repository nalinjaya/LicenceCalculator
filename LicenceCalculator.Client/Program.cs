using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LicenceCalculator.Repository;
using LicenceCalculator.Domain;

namespace LicenceCalculator.Client
{
    class Program
    {
        static void Main(string[] args)
        {

            var defaultPath = @"C:\LicenceCalculator\sample.csv";
            var defaultApplicationId = 374;

            Console.WriteLine("Please enter file path (Default:{0})", defaultPath);
            var input = Console.ReadLine();
            var path = string.IsNullOrWhiteSpace(input) ? defaultPath : input;

            Console.WriteLine("Reading file ...", defaultPath);
            var repo = new UserInstallationRequirementRepository(path);

            try
            {
                var usereInstallationRequirements = repo.GetByApplicationId(defaultApplicationId);
                Console.WriteLine("Calculating ...", defaultPath);
                var nuberOfLicencesRequired = LicenceCalculationService.CalculateRequiredNumberOfLicences(usereInstallationRequirements);
                Console.WriteLine("You need {0} licences", nuberOfLicencesRequired);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
           
            Console.Read();
        }
    }
}
