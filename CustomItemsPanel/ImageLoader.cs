using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using FlickrNet;

namespace CustomItemsPanel
{
    public static class ImageLoader
    {
        static readonly string defaultKeyword = ConfigurationManager.AppSettings["defaultKey"].ToString();
        public static string keyword { get; set; }
        public static string fileName { get; set; }
        //public static ObservableCollection<BitmapImage> observablenChange { get; set; }
        public static ObservableCollection<BitmapImage> LoadImages()
        {
            string currentKeyword = "";
            if (keyword != null)
            {
                currentKeyword = keyword;
            }
            else
            {
                currentKeyword = defaultKeyword;
            }

            PreviousSearchData previousSearchData = new PreviousSearchData(new ErrorLogFile());
            previousSearchData.CreateFile(currentKeyword, fileName);
            BusinessLogic businessLogic = new BusinessLogic();
            //List<BitmapImage> robotImages = new List<BitmapImage>();

            ObservableCollection<BitmapImage> robotImages = new ObservableCollection<BitmapImage>();
            IEnumerable collection = BusinessLogic.GetPhotos(currentKeyword);

            foreach (var robotImageFile in collection)
            {
                var xx = (Photo)robotImageFile;
                Uri uri = new Uri(xx.LargeUrl);
                robotImages.Add(new BitmapImage(uri));
            }

            return robotImages;
        }
    }
}
