using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomItemsPanel
{
    class PreviousSearchData
    {
        private IErrorLogger logger;
        public PreviousSearchData(IErrorLogger logger)
        {
            this.logger = logger;
        }
        BusinessLogic businessLogic = new BusinessLogic();
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
        public void ReadFile( string currentKeyword, string fileName, out string updateKeyword)
        {
            try
            {
                var lines = File.ReadAllLines(fileName).Reverse();
                foreach (string item in lines)
                {
                    if (!item.Equals(currentKeyword))
                    {
                        BusinessLogic.GetPhotos(item);
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
                //Console.WriteLine(Ex.ToString());
            }
        }
    }
}
