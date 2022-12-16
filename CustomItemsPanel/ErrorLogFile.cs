using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomItemsPanel
{
    class ErrorLogFile : IErrorLogger
    {
        static readonly string logErrorFileName = ConfigurationManager.AppSettings["LogErrorFileName"].ToString();
        public void LogError(Exception ex)
        {
            if (!Directory.Exists(logErrorFileName))
            {
                Directory.CreateDirectory(logErrorFileName);
            }

            FileStream fs = new FileStream(logErrorFileName, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(DateTime.Now + " Message : " + ex.Message);
            sw.Close();
            fs.Close();
        }
    }
}
