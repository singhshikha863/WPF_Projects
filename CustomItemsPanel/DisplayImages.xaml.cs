using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
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
            previousSearchData.PreviousSearchKey(SearchTextBox.Text.Trim(), fileName,out string updateKeyword);
            SearchTextBox.Text = updateKeyword;
            ImageLoader.keyword = SearchTextBox.Text.Trim();
            ImageCollection.ItemsSource = ImageLoader.LoadImages();
        }

        //  double currentScale = 1.0;
        double scale = 1.0;
        double minScale = 0.5;
        double maxScale = 2.0;
        private void onMouseWheel_Scroll(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            //var position = e.MouseDevice.GetPosition(ImageGrid);
            //var renderTransformValue = ImageGrid.RenderTransform.Value;
            //if (e.Delta > 0)
            //{
            //    currentScale += 0.1;
            //}
            //else if (e.Delta < 0)
            //{
            //    currentScale -= 0.1;
            //    if (currentScale < 1.0)
            //        currentScale = 1.0;
            //}
            //ImageGrid.RenderTransform = new ScaleTransform(currentScale, currentScale, position.X, position.Y);
            //ListBox.SelectedItemProperty
            ImageCollection.RenderTransform = null;

            var position = e.MouseDevice.GetPosition(ImageCollection);

            if (e.Delta > 0)
                scale += 0.1;
            else
                scale -= 0.1;

            if (scale > maxScale)
                scale = maxScale;
            if (scale < minScale)
                scale = minScale;

            ImageCollection.RenderTransform = new ScaleTransform(scale, scale, position.X, position.Y);
        }

        private void ImageCollection_Selected(object sender, RoutedEventArgs e)
        {

        }

        public System.Drawing.Image SelectedItemProp
        {
            get { return selectedItemProp; }
            set { selectedItemProp = value; }
        }

        private System.Drawing.Image selectedItemProp;
        private void ImageCollection_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Image image1 = (Image)sender;
            var image = ImageCollection.SelectedItem as System.Drawing.Image;
          //  SelectedImage.Source = ConvertImage(image);


        }

        public static ImageSource ConvertImage(System.Drawing.Image image)
        {
            try
            {
                if (image != null)
                {
                    var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                    bitmap.BeginInit();
                    System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                    image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                    bitmap.StreamSource = memoryStream;
                    bitmap.EndInit();
                    return bitmap;
                }
            }
            catch { }
            return null;
        }
        public string defaultText { get; set; }
        bool hasBeenClicked = false;
        string text = "Type your serach here";
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
                box.Text = text;
            }            
        }

    }
}