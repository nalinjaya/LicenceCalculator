using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenceCalculator.Domain
{
    public class LicenceCalculationService
    {
        private static int GetCountPerComputerType(UserInstallationRequirement installationRequirement, ComputerType type) =>
            installationRequirement.Installations.Where(i => i.Computer.ComputerType == type).Count();

        private static int CalculateRequiredNumberOfLicences(UserInstallationRequirement userInstallationRequirements)
        {
            decimal desktopLicencesRequired = GetCountPerComputerType(userInstallationRequirements, ComputerType.Desktop);
            decimal laptopLicencesRequired = GetCountPerComputerType(userInstallationRequirements, ComputerType.Laptop);

            if (desktopLicencesRequired >= laptopLicencesRequired) return (int)desktopLicencesRequired;

            var additionalLicencesRequired = (int)Math.Ceiling((laptopLicencesRequired - desktopLicencesRequired) / 2);

            return (int)desktopLicencesRequired + additionalLicencesRequired;
        }
        public static int CalculateRequiredNumberOfLicences(IEnumerable<UserInstallationRequirement> userInstallationRequirements) =>
             userInstallationRequirements.Select(CalculateRequiredNumberOfLicences).Sum();
    }
}
