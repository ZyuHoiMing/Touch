using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.UI.Animations;
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
            RefreshButton.Click += async (sender, args) => await RefreshAsync();
        }

        // TODO 考虑下怎么复用
        /// <summary>
        ///     根据窗口大小动态调整 item 长宽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridItem_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var grid = (ItemsWrapGrid) sender;
            if (VisualStateGroup.CurrentState == NarrowVisualState)
                grid.ItemWidth = e.NewSize.Width / 2;
            else if (VisualStateGroup.CurrentState == NormalVisualState)
                grid.ItemWidth = e.NewSize.Width / 3;
            else if (VisualStateGroup.CurrentState == WideVisualState)
                grid.ItemWidth = e.NewSize.Width / 4;
            grid.ItemHeight = grid.ItemWidth * 3 / 4;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await RefreshAsync();
        }

        /// <summary>
        ///     刷新图库图片
        /// </summary>
        /// <returns></returns>
        private async Task RefreshAsync()
        {
            LoadingControl.IsLoading = true;
            await _allImageListVm.RefreshAsync();
            Cvs.Source = _allImageListVm.ImageMonthGroups;
            LoadingControl.IsLoading = false;
        }

        /// <summary>
        ///     鼠标进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            var animation = (sender as FrameworkElement).Light(200);
            await animation.StartAsync();
        }

        /// <summary>
        ///     鼠标移出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            var animation = (sender as FrameworkElement).Light(500);
            await animation.StartAsync();
        }
    }
}