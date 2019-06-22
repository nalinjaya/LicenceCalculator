using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LicenceCalculator.Repository;
using LicenceCalculator.Domain;
using Serilog;

namespace LicenceCalculator.Client
{
    class Program
    {
        static void Main(string[] args)
        {

            Application.GetInstance().Run();
           
            Console.Read();
        }
    }
}
