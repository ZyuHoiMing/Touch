using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Touch.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GalleryPage : Page
    {
        private readonly ObservableCollection<GalleryGridItem> _galleryGridItems;

        public GalleryPage()
        {
            InitializeComponent();

            _galleryGridItems = new ObservableCollection<GalleryGridItem>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var item = new GalleryGridItem
            {
                ImageUrl = "ms-appx:///Assets/pic1.jpg"
            };
            _galleryGridItems.Add(item);
            item = new GalleryGridItem
            {
                ImageUrl = "ms-appx:///Assets/pic2.jpg"
            };
            _galleryGridItems.Add(item);
            item = new GalleryGridItem
            {
                ImageUrl = "ms-appx:///Assets/pic3.jpg"
            };
            _galleryGridItems.Add(item);
            item = new GalleryGridItem
            {
                ImageUrl = "ms-appx:///Assets/pic4.jpg"
            };
            _galleryGridItems.Add(item);
            item = new GalleryGridItem
            {
                ImageUrl = "ms-appx:///Assets/pic5.jpg"
            };
            _galleryGridItems.Add(item);
            item = new GalleryGridItem
            {
                ImageUrl = "ms-appx:///Assets/pic6.jpg"
            };
            _galleryGridItems.Add(item);
        }

        private void GridItem_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // 根据窗口大小动态调整 item 长宽
            var grid = (ItemsWrapGrid) sender;
            if (VisualStateGroup_Fuli.CurrentState == NarrowVisualState)
                grid.ItemWidth = e.NewSize.Width / 2;
            else if (VisualStateGroup_Fuli.CurrentState == NormalVisualState)
                grid.ItemWidth = e.NewSize.Width / 3;
            else if (VisualStateGroup_Fuli.CurrentState == WideVisualState)
                grid.ItemWidth = e.NewSize.Width / 4;
            grid.ItemHeight = grid.ItemWidth * 9 / 16;
        }
    }
}