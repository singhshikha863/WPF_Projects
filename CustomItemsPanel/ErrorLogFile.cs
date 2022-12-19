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
        private readonly string logErrorFileName = ConfigurationManager.AppSettings["LogErrorFileName"].ToString();
        public void LogError(Exception ex)
        {
            try
            {
                FileStream fileStream = null;
                DirectoryInfo logDirInfo = null;

                string logFilePath = logErrorFileName;
                logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
                var logFileInfo = new FileInfo(logFilePath);
                if (logFileInfo.DirectoryName != null) logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (logDirInfo != null && !logDirInfo.Exists) logDirInfo.Create();
                fileStream = !logFileInfo.Exists ? logFileInfo.Create() : new FileStream(logFilePath, FileMode.Append);
                var log = new StreamWriter(fileStream);
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
