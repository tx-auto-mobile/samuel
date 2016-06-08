using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadCsvFiles
{
    public class ReadCsvFiles
    {
        private static string capabilitiesPath = @"C:\Users\osmar.garcia\Documents\Visual Studio 2015\Projects\AccuWeather\ReadCsvFiles\CsvFiles\Capabilities.csv";






        //read CSV data
        private List<string> leerDatos(string path, int fila_start)
        {
            List<string> parsedData = new List<string>();

            using (StreamReader readFile = new StreamReader(path))
            {
                string line;
                string[] row;
                int cont = 0;
                

                while ((line = readFile.ReadLine()) != null)
                {
                    if (cont == fila_start)
                    {
                        row = line.Split(',');
                        parsedData.Add(row[0]);
                    }
                    cont++;
                }
            }
            return parsedData;
        }

        static void Main() {
            ReadCsvFiles x = new ReadCsvFiles();
            List<string> parsedData = x.leerDatos(capabilitiesPath, 1);
            for (int i= 0; i < parsedData.Count; i++)
            {
                System.Console.WriteLine(parsedData[i]);
            }
        }

    }

   




}
