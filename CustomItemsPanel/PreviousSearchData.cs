using System;
using System.IO;
using System.Linq;

namespace CustomItemsPanel
{
    /// <summary>
    /// This class contians the logic to retrive the previous search data
    /// </summary>
    public class PreviousSearchData
    {
        #region Private Member
        private IErrorLogger logger;
        #endregion

        #region Constructor
        public PreviousSearchData(IErrorLogger logger)
        {
            this.logger = logger;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This method will create the file to preserve the last search keywords
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="fileName"></param>
        public void CreateFile(string keyword, string fileName)
        {            
            try
            {               
                // Create a new file  
                if (!File.Exists(fileName))
                {
                    using (StreamWriter sw = File.CreateText(fileName))
                    {
                        sw.WriteLine(keyword);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(fileName))
                    {
                        sw.WriteLine(keyword);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }
        }

        /// <summary>
        /// This method will read the previous searched keyword and get the photos 
        /// </summary>
        /// <param name="currentKeyword"></param>
        /// <param name="fileName"></param>
        /// <param name="updateKeyword"></param>
        public void PreviousSearchKey( string currentKeyword, string fileName, out string updateKeyword)
        {
            try
            {
                var lines = File.ReadAllLines(fileName).Reverse();
                foreach (string item in lines)
                {
                    if (!item.Equals(currentKeyword))
                    {
                        updateKeyword = item;
                        return;
                    }
                }
                updateKeyword = currentKeyword;
            }
            catch (Exception ex)
            {
                updateKeyword = currentKeyword;
                logger.LogError(ex);
            }
        }

        #endregion
    }
}
