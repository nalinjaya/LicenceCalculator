using LicenceCalculator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenceCalculator.Repository
{
    public class LicenceInfoRow
    {
        public string ComputerId { get; set; }
        public string UserId { get; set; }
        public string ApplciationId { get; set; }
        public string ComputerType { get; set; }
        public string Comment { get; set; }
    }

    public class LicenceInfo
    {
        public int ComputerId { get; }
        public int UserId { get; }
        public int ApplicationId { get; }
        public ComputerType ComputerType { get; }

        public LicenceInfo(int userId, int computerId, int applicationId, ComputerType computerType)
        {
            UserId = userId;
            ComputerId = computerId;
            ApplicationId = applicationId;
            ComputerType = computerType;
        }
    }
}
