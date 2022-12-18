using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using System.Windows.Media.Imaging;
using FlickrNet;

namespace CustomItemsPanel
{
    /// <summary>
    /// This class contains the logic to load the images and present it to the user 
    /// </summary>
    public static class ImageLoader
    {
        #region Private Member

        private static readonly string defaultKeyword = ConfigurationManager.AppSettings["defaultKey"].ToString();
        #endregion

        #region Public Member
        public static string keyword { get; set; }
        public static string fileName { get; set; }

        #endregion

        #region Public Methods
        /// <summary>
        /// This method contains the logic to load the images and return it to the user 
        /// </summary>
        /// <returns></returns>
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
            BusinessLogic businessLogic = new BusinessLogic(new ErrorLogFile());           

            ObservableCollection<BitmapImage> collectionOfImages = new ObservableCollection<BitmapImage>();
            IEnumerable collection = businessLogic.GetPhotos(currentKeyword);
            if (collection != null)
            {
                foreach (var imageFile in collection)
                {
                    var extractURL = (Photo)imageFile;
                    Uri uri = new Uri(extractURL.LargeUrl);
                    collectionOfImages.Add(new BitmapImage(uri));
                }
            }
            else
            {
                MessageBox.Show("The collection result is null, please check the api calling or network correction" +MessageBoxImage.Error +MessageBoxButton.OK);
            }
            
            return collectionOfImages;
        }

        #endregion
    }
}
