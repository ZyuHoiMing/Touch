using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Touch.Views.UserControls
{
    public sealed partial class VideoButtonControl : UserControl
    {
        public event Action OnPlayButtonClicked;
        public event Action OnReplayButtonClicked;

        public VideoButtonControl()
        {
            InitializeComponent();
            ToggleAnimation(false);
            PlayButton.Click += (sender, arg) =>
            {
                OnPlayButtonClicked?.Invoke();
            };
            ReplayButton.Click += (sender, arg) =>
            {
                OnReplayButtonClicked?.Invoke();
            };
        }

        /// <summary>
        /// 显示播放按钮
        /// </summary>
        public void ShowPlayButton()
        {
            ReplayButton.Visibility = Visibility.Collapsed;
            PlayButton.Visibility = Visibility.Visible;
            ToggleAnimation(true);
        }

        /// <summary>
        /// 显示重播按钮
        /// </summary>
        public void ShowReplayButton()
        {
            PlayButton.Visibility = Visibility.Collapsed;
            ReplayButton.Visibility = Visibility.Visible;
            ToggleAnimation(true);
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public void Hide()
        {
            ToggleAnimation(false);
        }

        // TODO 复用
        private void ToggleAnimation(bool show)
        {
            var _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            var _detailGridVisual = ElementCompositionPreview.GetElementVisual(RootGrid);

            var fadeAnimation = _compositor.CreateScalarKeyFrameAnimation();
            fadeAnimation.InsertKeyFrame(1f, show ? 1f : 0f);
            fadeAnimation.Duration = TimeSpan.FromMilliseconds(700);

            _detailGridVisual.StartAnimation("Opacity", fadeAnimation);
        }
    }
}
