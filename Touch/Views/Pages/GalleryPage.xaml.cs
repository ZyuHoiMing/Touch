using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Touch.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public sealed partial class GalleryPage : Page
    {
        private readonly AllImageListViewModel _allImageListVm;

        public GalleryPage()
        {
            InitializeComponent();

            _allImageListVm = new AllImageListViewModel();

            //var group = from trigger in _allImageListVm.MyImageVms group trigger by trigger.DateTaken.Month;
        }

        // TODO 怎么考虑复用
        private void GridItem_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // 根据窗口大小动态调整 item 长宽
            var grid = (ItemsWrapGrid) sender;
            if (VisualStateGroup.CurrentState == NarrowVisualState)
                grid.ItemWidth = e.NewSize.Width / 2;
            else if (VisualStateGroup.CurrentState == NormalVisualState)
                grid.ItemWidth = e.NewSize.Width / 3;
            else if (VisualStateGroup.CurrentState == WideVisualState)
                grid.ItemWidth = e.NewSize.Width / 4;
            grid.ItemHeight = grid.ItemWidth * 9 / 16;
        }

        private async void GalleryPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            await _allImageListVm.RefreshAsync();
        }
    }
}