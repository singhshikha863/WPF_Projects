using System;
using CustomItemsPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomItemsPanelTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MockData dd = new MockData();
            var y = dd.GetPhotos("Orange");

            NUnit.Framework.Assert.IsNotNull(y);

        }

        [TestMethod]
        public void TestPreviousSerach()
        {
            MockData dd = new MockData();
            string fileName = @"C:\Temp\Keyword.txt";
            //dd.PreviousSearchKey(string currentKeyword, string fileName, out string updateKeyword)
            dd.PreviousSearchKey("Orange", fileName, out string updateKeyword);
            NUnit.Framework.Assert.AreNotEqual("Orange", updateKeyword);
        }
    }

   
}
