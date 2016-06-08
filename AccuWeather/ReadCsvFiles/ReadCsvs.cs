using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReadCsvFiles
{
    public class ReadCsvs
    {
        private static string capabilitiesPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\CsvFiles\Capabilities.csv";
        private static string appiumConfiguration = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\CsvFiles\AppiumConfiguration.csv";
        private static string sauceLabsConfiguration = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\CsvFiles\SauceLabsConfiguration.csv";

        //@"C:\Users\osmar.garcia\Documents\Visual Studio 2015\Projects\AccuWeather\ReadCsvFiles\CsvFiles\Capabilities.csv";






        //read CSV data
        private List<string> readData(string path, int fila_start)
        {
            List<string> parsedData = new List<string>();

            using (StreamReader readFile = new StreamReader(path))
            {
                string line;
                string[] row;
                int cont = 0;

                while ((line = readFile.ReadLine()) != null)
                {
                    if (cont == fila_start) {
                        row = line.Split(',');
                        for (int i = 0; i < row.Length; i++)
                        {
                            parsedData.Add(row[i]);
                        }
                    }

                    if (cont == fila_start)
                    {
                        cont++;
                        fila_start++;

                    }
                    else
                    {
                        cont++;
                    }
                    
                }
            }
            return parsedData;
        }

        public List<string> readCapabilities()
        {
            return readData(capabilitiesPath, 1);
        }
        public List<string> readAppiumConfiguration()
        {
            return readData(appiumConfiguration, 1);
        }
        public List<string> readSauceLabsConfiguration()
        {
            return readData(sauceLabsConfiguration, 1);
        }
        //read capabilities


        static void Main() {
            ReadCsvs x = new ReadCsvs();
            List<string> parsedData = x.readData(capabilitiesPath, 1);
            for (int i= 0; i < parsedData.Count; i++)
            {
                System.Console.WriteLine(parsedData[i]);
            }
        }

    }

   




}
