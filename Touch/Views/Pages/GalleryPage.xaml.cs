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
        }
    }
}
