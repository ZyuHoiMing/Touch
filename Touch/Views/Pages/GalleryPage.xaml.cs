using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Touch.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GalleryPage : Page
    {
        private ObservableCollection<GalleryGridItem> galleryGridItems;

        public GalleryPage()
        {
            this.InitializeComponent();

            galleryGridItems = new ObservableCollection<GalleryGridItem>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GalleryGridItem item = new GalleryGridItem()
            {
                ImageUrl = "ms-appx:///Assets/pic1.jpg"
            };
            galleryGridItems.Add(item);
            item = new GalleryGridItem()
            {
                ImageUrl = "ms-appx:///Assets/pic2.jpg"
            };
            galleryGridItems.Add(item);
            item = new GalleryGridItem()
            {
                ImageUrl = "ms-appx:///Assets/pic3.jpg"
            };
            galleryGridItems.Add(item);
            item = new GalleryGridItem()
            {
                ImageUrl = "ms-appx:///Assets/pic4.jpg"
            };
            galleryGridItems.Add(item);
            item = new GalleryGridItem()
            {
                ImageUrl = "ms-appx:///Assets/pic5.jpg"
            };
            galleryGridItems.Add(item);
            item = new GalleryGridItem()
            {
                ImageUrl = "ms-appx:///Assets/pic6.jpg"
            };
            galleryGridItems.Add(item);
        }
        
        private void GridItem_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // 根据窗口大小动态调整 item 长宽
            var grid = (ItemsWrapGrid)sender;
            if (VisualStateGroup_Fuli.CurrentState == NarrowVisualState)
            {
                grid.ItemWidth = e.NewSize.Width / 2;
            }
            else if (VisualStateGroup_Fuli.CurrentState == NormalVisualState)
            {
                grid.ItemWidth = e.NewSize.Width / 3;
            }
            else if (VisualStateGroup_Fuli.CurrentState == WideVisualState)
            {
                grid.ItemWidth = e.NewSize.Width / 4;
            }
            grid.ItemHeight = grid.ItemWidth * 9 / 16;
        }
    }
}
