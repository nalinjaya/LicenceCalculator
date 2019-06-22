using LicenceCalculator.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LicenceCalculator.Tests
{
    [TestClass]
    public class LicenceCalculationServiceTests
    {
        
        [TestMethod]
        public void Given_1_Desktop_Should_Require_1_Licence()
        {
            var installations = new [] { new Installation(new Computer(1, ComputerType.Desktop), new Application(314)) };
            var requirements = new[] { new UserInstallationRequirement(1, installations) };
            var actual = new LicenceCalculationService().CalculateRequiredNumberOfLicences(requirements);
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void Given_1_Desktop_And_1_Laptop_Should_Require_One_Licence()
        {
            var installations = new[] {
                new Installation(new Computer(1, ComputerType.Desktop), new Application(314)),
                new Installation(new Computer(2, ComputerType.Laptop), new Application(314))
            };
            var requirements = new[] { new UserInstallationRequirement(1, installations) };
            var actual = new LicenceCalculationService().CalculateRequiredNumberOfLicences(requirements);
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void Given_2_Desktops_Should_Require_2_Licences()
        {
            var installations = new[] {
                new Installation(new Computer(1, ComputerType.Desktop), new Application(314)),
                new Installation(new Computer(2, ComputerType.Desktop), new Application(314))
            };
            var requirements = new[] { new UserInstallationRequirement(1, installations) };
            var actual = new LicenceCalculationService().CalculateRequiredNumberOfLicences(requirements);
            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void Given_2_Desktops_And_3_Laptops_Should_Require_3_Licences()
        {
            var installations = new[] {
                new Installation(new Computer(1, ComputerType.Desktop), new Application(314)),
                new Installation(new Computer(2, ComputerType.Desktop), new Application(314)),
                new Installation(new Computer(3, ComputerType.Laptop), new Application(314)),
                new Installation(new Computer(4, ComputerType.Laptop), new Application(314)),
                new Installation(new Computer(5, ComputerType.Laptop), new Application(314))
            };
            var requirements = new[] { new UserInstallationRequirement(1, installations) };
            var actual = new LicenceCalculationService().CalculateRequiredNumberOfLicences(requirements);
            Assert.AreEqual(3, actual);
        }

        [TestMethod]
        public void Given_2_Desktops_And_3_Laptops_For_2_Users_Should_Require_6_Licences()
        {
            var installations = new[] {
                new Installation(new Computer(1, ComputerType.Desktop), new Application(314)),
                new Installation(new Computer(2, ComputerType.Desktop), new Application(314)),
                new Installation(new Computer(3, ComputerType.Laptop), new Application(314)),
                new Installation(new Computer(4, ComputerType.Laptop), new Application(314)),
                new Installation(new Computer(5, ComputerType.Laptop), new Application(314))
            };
            var requirements = new[] 
                { new UserInstallationRequirement(1, installations),
                  new UserInstallationRequirement(2, installations)
            };
            var actual = new LicenceCalculationService().CalculateRequiredNumberOfLicences(requirements);
            Assert.AreEqual(6, actual);
        }
    }
}
