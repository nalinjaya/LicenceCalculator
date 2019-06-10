using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenceCalculator.Domain
{
    public enum ComputerType { None, Desktop, Laptop}

    public class Application
    {
        public int ApplicationId { get; }

        public Application(int applicationId)
        {
            ApplicationId = applicationId;
        }
    }

    public class Computer
    {
        public int ComputerId { get; }
        public ComputerType ComputerType { get; }
        public Computer(int deviceId, ComputerType computerType) {
            ComputerId = deviceId;
            ComputerType = computerType;
        }
    }

    public class Installation
    {
        public Installation (Computer computer, Application application)
        {
            Computer = computer;
            Application = application;
        }
        public Computer Computer { get;  }
        public Application Application { get; }

    }

    public class UserInstallationRequirement
    {
        public int UserId { get; }
        public IEnumerable<Installation> Installations { get; }

        public UserInstallationRequirement (int userId, IEnumerable<Installation> installations)
        {
            UserId = userId;
            Installations = installations;
        }
    }
}
