using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomItemsPanel
{
    class ErrorLogFile : IErrorLogger
    {
        public void LogError(Exception ex)
        {
            string folderPath = "C:\\Temp";
            // private string file = folderPath + "Data.txt";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            FileStream fs = new FileStream(folderPath + "ErrrLog.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(DateTime.Now + " Message : " + ex.Message);
            sw.Close();
            fs.Close();
        }
    }
}
