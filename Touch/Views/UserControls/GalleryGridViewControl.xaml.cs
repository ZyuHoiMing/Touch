using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Touch.ViewModels;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Touch.Views.UserControls
{
    // ReSharper disable once RedundantExtendsListEntry
    public sealed partial class GalleryGridViewControl : UserControl
    {
        private GalleryImageListViewModel _galleryImageListViewModel;
        private bool _isLoaded;

        public GalleryGridViewControl()
        {
            InitializeComponent();
            _isLoaded = false;
        }

        /// <summary>
        ///     选中的viewmodels
        /// </summary>
        public List<ImageViewModel> SelectedImageViewModels
            => GalleryGridView.SelectedItems.Cast<ImageViewModel>().ToList();

        /// <summary>
        ///     刷新图库
        /// </summary>
        public async Task RefreshAsync()
        {
            if (_galleryImageListViewModel == null)
                return;
            Debug.WriteLine("RefreshAsync---" + "_galleryImageListViewModel.RefreshFolderListAsync()");
            await _galleryImageListViewModel.RefreshFolderListAsync();
            Debug.WriteLine("RefreshAsync---" + "_galleryImageListViewModel.RefreshImageListAsync()");
            await _galleryImageListViewModel.RefreshImageListAsync();
            Debug.WriteLine("RefreshAsync---" + "Cvs.Source");
            Cvs.Source = _galleryImageListViewModel.ImageMonthGroups;
        }

        /// <summary>
        ///     把gridview设置为多选
        /// </summary>
        public void SetGridViewMultipleSelection()
        {
            GalleryGridView.SelectionMode = ListViewSelectionMode.Multiple;
            GalleryGridView.IsItemClickEnabled = false;
        }

        /// <summary>
        ///     把gridview设置为点击
        /// </summary>
        public void SetGridViewClickable()
        {
            GalleryGridView.SelectionMode = ListViewSelectionMode.None;
            GalleryGridView.IsItemClickEnabled = true;
        }

        private async void GalleryGridViewControl_OnLoading(FrameworkElement sender, object args)
        {
            if (_isLoaded)
                return;
            Debug.WriteLine("GalleryGridViewControl_OnLoading---" + "GalleryImageListViewModel.GetInstanceAsync()");
            _galleryImageListViewModel = await GalleryImageListViewModel.GetInstanceAsync();
            Debug.WriteLine("GalleryGridViewControl_OnLoading---" + "Cvs.Source");
            Cvs.Source = _galleryImageListViewModel.ImageMonthGroups;
            _isLoaded = true;
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
        private void GridViewItem_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            var rootGrid = sender as Grid;
            if (rootGrid == null)
                return;
            var img = rootGrid.Children[0] as FrameworkElement;
            ToggleItemPointAnimation(img, true);
        }

        /// <summary>
        ///     鼠标移出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewItem_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            var rootGrid = sender as Grid;
            if (rootGrid == null)
                return;
            var img = rootGrid.Children[0] as FrameworkElement;
            ToggleItemPointAnimation(img, false);
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