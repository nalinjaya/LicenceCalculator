using LicenceCalculator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenceCalculator.Repository
{
    public class UserInstallationRequirementRepository
    {
        private IList<UserInstallationRequirement> _userInstallationRequirements;
        private readonly string _filePath;
        public UserInstallationRequirementRepository(string filePath)
        {
            _filePath = filePath;
        }
        public IEnumerable<UserInstallationRequirement> GetByApplicationId(int applicationId) =>
            GetUserInstallationRequirements(applicationId);

        private ComputerType ParseComputerType(string computerType)
        {
            computerType = computerType.ToLower();
            if (computerType == "desktop")
                return ComputerType.Desktop;
            if (computerType == "laptop")
                return ComputerType.Laptop;
            return ComputerType.None;
        }

        private IEnumerable<UserInstallationRequirement> GetUserInstallationRequirements(int applicationId)
        {
            if (_userInstallationRequirements == null)
                _userInstallationRequirements = MapToDomain(LicenceInfoCsvReader.Read(_filePath, true)
                    .Where(l => l.ApplciationId == applicationId.ToString())).ToList();
            return _userInstallationRequirements;
        }

        private LicenceInfo Validate(LicenceInfoRow licenceInfo) =>
            (int.TryParse(licenceInfo.UserId, out int uid) &&
               int.TryParse(licenceInfo.ComputerId, out int cid) &&
               int.TryParse(licenceInfo.ApplciationId, out int aid) &&
               ParseComputerType(licenceInfo.ComputerType) != ComputerType.None) ? new LicenceInfo(uid, cid, aid, ParseComputerType(licenceInfo.ComputerType)) : null;

        private IEnumerable<LicenceInfo> GetValidLicenceInfo(IEnumerable<LicenceInfoRow> licenceInfoRows) =>
               licenceInfoRows
                    .Distinct()
                    .Select(Validate)
                    .Where(l => l != null);
        private IEnumerable<UserInstallationRequirement> MapToDomain(IEnumerable<LicenceInfoRow> licenceInfos)
        {
            var validLicenceInfos = GetValidLicenceInfo(licenceInfos);

            return
            from validLicenceInfo in validLicenceInfos 
            group validLicenceInfo by new { validLicenceInfo.UserId } into grp
            select new UserInstallationRequirement
            (
                grp.Key.UserId,
                validLicenceInfos.Where(l => l.UserId == grp.Key.UserId).Select(a => 
                new Installation(
                    new Computer(a.ComputerId, a.ComputerType),
                    new Application(a.ApplicationId)))
            );
        }
    }
}

