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
    public sealed partial class BackButtonControl : UserControl
    {
        public event Action OnBackButtonClicked;

        public BackButtonControl()
        {
            this.InitializeComponent();

            BackButton.Click += (sender, args) =>
            {
                OnBackButtonClicked?.Invoke();
            };
        }

        /// <summary>
        /// 返回按钮的隐藏动画
        /// </summary>
        /// <param name="show"></param>
        private void ToggleTitleStackAnimation(bool show)
        {
            var offsetAnimation = ElementCompositionPreview.GetElementVisual(this).Compositor.CreateScalarKeyFrameAnimation();
            offsetAnimation.InsertKeyFrame(1f, show ? 144f : 0f);
            offsetAnimation.Duration = TimeSpan.FromMilliseconds(500);
            ElementCompositionPreview.GetElementVisual(TitleStack).StartAnimation("Offset.X", offsetAnimation);
        }

        private void BackButtonGrid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ToggleTitleStackAnimation(true);
        }

        private void BackButtonGrid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ToggleTitleStackAnimation(false);
        }
    }
}
