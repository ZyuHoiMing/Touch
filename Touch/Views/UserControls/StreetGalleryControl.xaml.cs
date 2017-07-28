using Touch.ViewModels;
using System;
using Touch.Controls;
using System.Diagnostics;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Touch.Views.UserControls
{
    public sealed partial class StreetGalleryControl : NavigableUserControl
    {
        public StreetImageListViewModel StreetImageListViewModel;
        public event Action OnBackButtonClicked;

        public StreetGalleryControl()
        {
            InitializeComponent();
            StreetImageListViewModel = new StreetImageListViewModel();
            // 左右button的点击事件
            LeftButton.Click += (sender, args) => { StreetImageListViewModel.SelectedIndex--; };
            RightButton.Click += (sender, args) => { StreetImageListViewModel.SelectedIndex++; };

            GalleryBackButtonControl.OnBackButtonClicked += () =>
            {
                Shown = false;
                OnBackButtonClicked?.Invoke();
            };
            
            Carousel.SelectionChanged += (sender, arg) =>
            {
                BackgroundDownImage.Source = StreetImageListViewModel.ImageViewModels[Carousel.SelectedIndex].ThumbnailImage;
                // 准备动画并播放
                var storyboard = new Storyboard();
                // 背景切换模糊
                storyboard.Children.Add(GetOpacityAnimation(BackgroundUpImage, true));
                storyboard.Begin();
                storyboard.Completed += (_sender, _e) =>
                {
                    // 设置背景图上层图片
                    BackgroundUpImage.Source = StreetImageListViewModel.ImageViewModels[Carousel.SelectedIndex].ThumbnailImage;
                };
            };
        }

        /// <summary>
        /// 设置背景图片
        /// </summary>
        public void SetBackground()
        {
            BackgroundUpImage.Source = StreetImageListViewModel.ImageViewModels[Carousel.SelectedIndex].ThumbnailImage;
            BackgroundDownImage.Source = StreetImageListViewModel.ImageViewModels[Carousel.SelectedIndex].ThumbnailImage;
        }

        /// <summary>
        ///     获取渐变动画
        /// </summary>
        /// <param name="target"></param>
        /// <param name="isFade"></param>
        /// <returns></returns>
        private DoubleAnimation GetOpacityAnimation(DependencyObject target, bool isFade)
        {
            DoubleAnimation opacityAnimation;
            if (isFade)
                opacityAnimation = new DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    Duration = new Duration(TimeSpan.FromMilliseconds(500))
                };
            else
                opacityAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = new Duration(TimeSpan.FromMilliseconds(500))
                };
            Storyboard.SetTargetProperty(opacityAnimation, "(UIElement.Opacity)");
            Storyboard.SetTarget(opacityAnimation, target);
            return opacityAnimation;
        }
    }
}