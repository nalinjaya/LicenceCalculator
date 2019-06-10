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
                var rows = new List<LicenceInfoRow>();
                if (hasHeaders)
                    reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (values.Length == 5)

                        rows.Add(new LicenceInfoRow
                        {
                            ComputerId = values[0],
                            UserId = values[1],
                            ApplciationId = values[2],
                            ComputerType = values[3],
                            Comment = values[4]
                        });
                }
                return rows;
            }
        }

        public static IEnumerable<LicenceInfoRow> Read(string filePath, bool hasHeaders, int applicationId)
        {
            using (var reader = new StreamReader(filePath))
            {
                var rows = new List<LicenceInfoRow>();  
                if (hasHeaders)
                    reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (values.Length == 5 && values[2] == applicationId.ToString())

                        rows.Add(new LicenceInfoRow
                        {
                            ComputerId = values[0],
                            UserId = values[1],
                            ApplciationId = values[2],
                            ComputerType = values[3],
                            Comment = values[4]
                        });
                }
                return rows;
            }
        }
    }
}
