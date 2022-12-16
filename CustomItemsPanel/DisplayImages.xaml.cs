using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FlickrNet;
using System.Configuration;
using System.Runtime.CompilerServices;
using Path = System.IO.Path;

namespace CustomItemsPanel
{
	public partial class Window1 : Window
	{
        readonly string fileName = ConfigurationManager.AppSettings["fileName"].ToString();
        private readonly PreviousSearchData previousSearchData = new PreviousSearchData(new ErrorLogFile());
        public Window1()
		{
            // Check if file already exists. If yes, delete it.  
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            RobotImageLoader.fileName = fileName;
            InitializeComponent();
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {

            RobotImageLoader.keyword = SearchTextBox.Text.Trim();
            ImageCollection.ItemsSource  = RobotImageLoader.LoadImages();
           // ImageCollection.Items.Refresh();
        }

        private void PreviousSearchClick(object sender, RoutedEventArgs e)
        {
            previousSearchData.ReadFile(SearchTextBox.Text.Trim(), fileName,out string updateKeyword);
            SearchTextBox.Text = updateKeyword;
            RobotImageLoader.keyword = SearchTextBox.Text.Trim();
            ImageCollection.ItemsSource = RobotImageLoader.LoadImages();
        }
    }
   // public static 

    public static class RobotImageLoader
	{
        static  readonly string defaultKeyword = ConfigurationManager.AppSettings["defaultKey"].ToString();
        public static string keyword { get; set; }
        public static string fileName { get; set; }
        //public static ObservableCollection<BitmapImage> observablenChange { get; set; }
        public static ObservableCollection<BitmapImage> LoadImages()
        {
            string currentKeyword = "";
            if (keyword!= null)
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