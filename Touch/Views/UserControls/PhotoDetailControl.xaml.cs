using CompositionHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Touch.ViewModels;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Popups;
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
    public sealed partial class PhotoDetailControl : UserControl
    {
        public event Action OnHide;
        
        public ImageViewModel PhotoDetailImageViewModel;
        
        public PhotoDetailControl()
        {
            InitializeComponent();
            ToggleDetailGridAnimation(false);
        }

        /// <summary>
        /// Toggle the enter animation by passing a list item. This control will take care of the rest part.
        /// </summary>
        public void Show()
        {
            DetailImage.Source = PhotoDetailImageViewModel.ThumbnailImage;
            ToggleDetailGridAnimation(true);
        }
        
        private void ToggleDetailGridAnimation(bool show)
        {
            var _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            var _detailGridVisual = ElementCompositionPreview.GetElementVisual(DetailGrid);

            var fadeAnimation = _compositor.CreateScalarKeyFrameAnimation();
            fadeAnimation.InsertKeyFrame(1f, show ? 1f : 0f);
            fadeAnimation.Duration = TimeSpan.FromMilliseconds(show ? 700 : 300);
            fadeAnimation.DelayTime = TimeSpan.FromMilliseconds(show ? 400 : 0);

            var batch = _compositor.CreateScopedBatch(CompositionBatchTypes.Animation);
            _detailGridVisual.StartAnimation("Opacity", fadeAnimation);

            batch.End();
        }

        private void MaskBorder_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            var batch = _compositor.CreateScopedBatch(CompositionBatchTypes.Animation);
            batch.Completed += (s, ex) =>
            {
                var innerBatch = _compositor.CreateScopedBatch(CompositionBatchTypes.Animation);
                innerBatch.Completed += (ss, exx) =>
                {
                    ToggleDetailGridAnimation(false);
                };
                innerBatch.End();
            };
            batch.End();
            OnHide.Invoke();
            PhotoDetailImageViewModel = null;
        }

        private void DetailGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var grid = sender as Grid;
            var gridWidth = grid.ActualWidth;
            var gridHeight = grid.ActualHeight;
            DetailImage.Width = gridWidth * 0.618;
            DetailImage.Height = gridHeight * 0.618;
        }
    }
}
