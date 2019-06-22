using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenceCalculator.Repository
{
    class LicenceInfoCsvReader
    {
        public static IEnumerable<LicenceInfoRow> Read(string filePath, bool hasHeaders)
        {
            using (var reader = new StreamReader(filePath))
            {
                if (hasHeaders)
                    reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (values.Length == 5)

                        yield return new LicenceInfoRow
                        {
                            ComputerId = values[0],
                            UserId = values[1],
                            ApplciationId = values[2],
                            ComputerType = values[3],
                            Comment = values[4]
                        };
                }
            }
        }
    }
}
