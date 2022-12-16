using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Configuration;

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

            ImageLoader.fileName = fileName;
            InitializeComponent();
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {

            ImageLoader.keyword = SearchTextBox.Text.Trim();
            ImageCollection.ItemsSource  = ImageLoader.LoadImages();
           // ImageCollection.Items.Refresh();
        }

        private void PreviousSearchClick(object sender, RoutedEventArgs e)
        {
            previousSearchData.ReadFile(SearchTextBox.Text.Trim(), fileName,out string updateKeyword);
            SearchTextBox.Text = updateKeyword;
            ImageLoader.keyword = SearchTextBox.Text.Trim();
            ImageCollection.ItemsSource = ImageLoader.LoadImages();
        }
    }
}