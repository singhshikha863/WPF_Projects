using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomItemsPanel
{
   public class ErrorLogFile : IErrorLogger
    {
        static readonly string logErrorFileName = ConfigurationManager.AppSettings["LogErrorFileName"].ToString();
        public void LogError(Exception ex)
        {
            try
            {
                StreamWriter log;
                FileStream fileStream = null;
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo;

                string logFilePath = logErrorFileName;
                logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
                logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }
                log = new StreamWriter(fileStream);
                log.Flush();
                log.WriteLine(DateTime.Now + " Message : " + ex.Message);
                log.Close();
            }
            catch (Exception)
            {
                throw;
            }           
        }
    }
}
