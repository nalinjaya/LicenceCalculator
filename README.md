# LicenceCalculator
A simple program to demonstrate some basic OO principals using a hypothetical problem of calculating licences requirements for an application based on the data provided in CSV format.

### Tools & Libraries
* IDE : Microsoft Visual Studio Community 2019
* Language : C#
* Target Framework : .NET Framework 4.7.2
* DI : [Autofac](https://autofac.org/)
* Logging : [Serilog](https://serilog.net/)
* Testing : Microsoft VisualStudio Test Framework

### How to Test?
* Please run project LicenceCalculator.Client to run the application.
* All unit tests could be found under the project LicenceCalculator.Tests.

### Notes
* Some principals of Domain Driven Design has been used in the solution. ChargeCalculator.Domain includes all the domain logic and is not poluted by underlying technical details such as where the data is retrieved from or what technologies used for that.
* A Simple logging mechanism has been implemented for the purpose of demonstration. No abstractions created over ILogger provided by Serilog. 
* Some classes are over-engineered a bit to demonstrate some of the patterns and usage of object oriented principles.
* Test coverage is limited as this is only for demo pupposes
