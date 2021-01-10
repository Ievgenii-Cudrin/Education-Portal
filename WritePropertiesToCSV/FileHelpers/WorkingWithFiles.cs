using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WritePropertiesToCSV.FileHelpers
{
    public static class WorkingWithFiles
    {
        public static void AddRecord(string data, string path)
        {
            try
            {
                //write to csv
                using (StreamWriter file = new StreamWriter(path, true))
                {
                    file.WriteLine(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
