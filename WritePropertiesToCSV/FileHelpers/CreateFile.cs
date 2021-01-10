using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WritePropertiesToCSV.FileHelpers
{
    public static class CreateFile
    {
        public static void CreateFileCSV(string path)
        {
            //create file
            var file = new FileInfo(path);

            FileStream stream;

            if (!file.Exists)
            {
                stream = file.Create();
            }
            else
            {
                return;
            }

            stream.Close();
        }
    }
}
