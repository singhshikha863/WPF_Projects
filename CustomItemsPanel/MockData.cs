using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomItemsPanel
{
    public class MockData
    {
        public IEnumerable GetPhotos(string cacheKey)
        {
            BusinessLogic businessLogic = new BusinessLogic( new ErrorLogFile());
            return businessLogic.GetPhotos(cacheKey);
        }

        public void PreviousSearchKey(string currentKeyword, string fileName, out string updateKeyword)
        {
            PreviousSearchData previousSearchData = new PreviousSearchData(new ErrorLogFile());
            previousSearchData.PreviousSearchKey(currentKeyword, fileName, out updateKeyword);
        }

    }
}
