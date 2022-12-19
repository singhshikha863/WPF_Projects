using System;
using System.IO;
using System.Windows;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace CustomItemsPanel
{
    public partial class Window1 : Window
    {
        #region MyRegion
        private bool browse = false;
        private bool previousSearch = false;
        private double scale = 1.0;
        private const double minScale = 0.5;
        private const double maxScale = 2.0;
        private bool hasBeenClicked = false;
        private string text = "Type your search here";
        private readonly string fileName = ConfigurationManager.AppSettings["fileName"].ToString();
        private string defaultText { get; set; }
        #endregion

        #region Constructor
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
        
        #endregion
        

        #region Private Methods

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            browse = true;
            ImageLoader.keyword = SearchTextBox.Text.Trim();
            ImageCollection.ItemsSource = ImageLoader.LoadImages();
            browse = false;

        }

        private void PreviousSearchClick(object sender, RoutedEventArgs e)
        {
            previousSearch = true;
            PreviousSearchData previousSearchData = new PreviousSearchData(new ErrorLogFile());
            previousSearchData.PreviousSearchKey(SearchTextBox.Text.Trim(), fileName, out string updateKeyword);
            SearchTextBox.Text = updateKeyword;
            ImageLoader.keyword = SearchTextBox.Text.Trim();
            ImageCollection.ItemsSource = ImageLoader.LoadImages();
            previousSearch = false;
        }

        private void onMouseWheel_Scroll(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            SelectedImage.RenderTransform = null;

            var position = e.MouseDevice.GetPosition(SelectedImage);

            if (e.Delta > 0)
                scale += 0.1;
            else
                scale -= 0.1;

            if (scale > maxScale)
                scale = maxScale;
            if (scale < minScale)
                scale = minScale;

            SelectedImage.RenderTransform = new ScaleTransform(scale, scale, position.X, position.Y);
        }

        private void ImageCollection_Selected(object sender, RoutedEventArgs e)
        {
            if (sender is ListBox lb) SelectedImage.Source = (ImageSource)lb.SelectedItem;
            SelectedImage.HorizontalAlignment = HorizontalAlignment.Center;
            SelectedImage.VerticalAlignment = VerticalAlignment.Center;
            if (!previousSearch && !browse)
            {
                ShowSelectedImage.Visibility = Visibility.Visible;
            }
        }

       
        private void TextBox_Focus(object sender, RoutedEventArgs e)
        {
            defaultText = SearchTextBox.Text;
            if (defaultText.Equals(text))
            {
                TextBox box = sender as TextBox;
                box.Text = String.Empty;
                hasBeenClicked = true;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (SearchTextBox.Text == "")
            {
                if (box != null) box.Text = text;
            }
        }

        private void ShowSelectedImage_OnMouseLeave(object sender, MouseEventArgs e)
        {
            ShowSelectedImage.Visibility = Visibility.Hidden;
        }

        
        #endregion

    }
}