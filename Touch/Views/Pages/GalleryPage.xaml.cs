using System;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

            LoadingControl.IsLoading = true;
            _allImageListVm = new AllImageListViewModel();
            RefreshButton.Click += async (sender, args) => await RefreshAsync();
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
            await _allImageListVm.RefreshAsync();
            Cvs.Source = _allImageListVm.ImageMonthGroups;
            LoadingControl.IsLoading = false;
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

        /// <summary>
        ///     item大小变化时需要对内容裁剪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewItem_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var rootGrid = sender as Grid;
            if (rootGrid != null)
                rootGrid.Clip = new RectangleGeometry
                {
                    Rect = new Rect(0, 0, rootGrid.ActualWidth, rootGrid.ActualHeight)
                };
        }

        /// <summary>
        ///     鼠标进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GridViewItem_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            var rootGrid = sender as Grid;
            if (rootGrid == null)
                return;
            var img = rootGrid.Children[0] as FrameworkElement;
            ToggleItemPointAnimation(img, true);
            var animation = rootGrid.Light(250);
            await animation.StartAsync();
        }

        /// <summary>
        ///     鼠标移出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GridViewItem_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            var rootGrid = sender as Grid;
            if (rootGrid == null)
                return;
            var img = rootGrid.Children[0] as FrameworkElement;
            ToggleItemPointAnimation(img, false);
            var animation = rootGrid.Light(500);
            await animation.StartAsync();
        }

        private void ToggleItemPointAnimation(FrameworkElement img, bool show)
        {
            var imgVisual = ElementCompositionPreview.GetElementVisual(img);
            var scaleAnimation = CreateScaleAnimation(show);
            imgVisual.CenterPoint = new Vector3((float) img.ActualWidth / 2, (float) img.ActualHeight / 2, 0f);
            imgVisual.StartAnimation("Scale.x", scaleAnimation);
            imgVisual.StartAnimation("Scale.y", scaleAnimation);
        }

        private ScalarKeyFrameAnimation CreateScaleAnimation(bool show)
        {
            var compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            var scaleAnimation = compositor.CreateScalarKeyFrameAnimation();
            scaleAnimation.InsertKeyFrame(1f, show ? 1.1f : 1f);
            scaleAnimation.Duration = TimeSpan.FromMilliseconds(1000);
            scaleAnimation.StopBehavior = AnimationStopBehavior.LeaveCurrentValue;
            return scaleAnimation;
        }
    }
}