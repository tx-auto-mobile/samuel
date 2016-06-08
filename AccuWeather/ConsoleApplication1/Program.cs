using ReadCsvFiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)

        {
            /*
             string path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
             string path1 = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("bin"));
             string path2 = new DirectoryInfo(Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("bin"))).Parent.FullName;
             string path3 = Path.Combine(Environment.CurrentDirectory, @"Data\", "osmar.txt");

  
     string path4 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)+ @"\Debug\CsvFiles\Capabilities.csv";
          
            */

            ReadCsvs x = new ReadCsvs();
                        List<string> parsedData = x.readCapabilities();
                        for (int i = 0; i < parsedData.Count; i++)
                        {
                            System.Console.WriteLine(parsedData[i]);
                        }
                  

            Console.ReadKey();
        }
    }
}
