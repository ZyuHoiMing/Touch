using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StreetGalleryPage : Page
    {
        /// <summary>
        /// 图片panel的宽度
        /// </summary>
        private double panelWidth;
        /// <summary>
        /// 两个图片的间距
        /// </summary>
        private int imageMargin;
        /// <summary>
        /// 每个图片长宽
        /// </summary>
        private double imageSize;
        /// <summary>
        /// 图片间的间隔
        /// </summary>
        private Thickness margin;

        private ImageRecycleList imagePathList;
        private List<Image> imageList;
        /// <summary>
        /// 读取循环队列里的第几个图片
        /// </summary>
        private int imageCount = 0;
        private bool isAnimateFinished = true;

        public StreetGalleryPage()
        {
            this.InitializeComponent();

            // TODO image应该从data里拿
            List<string> paths = new List<string>
            {
                "ms-appx:///Assets/pic1_square.jpg"
            };
            imagePathList = new ImageRecycleList(paths);
            imageList = new List<Image>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitImageAttribute();

            // 中心图片
            var centerImage = GetImage(imagePathList.GetItem(imageCount++));
            centerImage.SetValue(RelativePanel.AlignVerticalCenterWithPanelProperty, true);
            centerImage.SetValue(RelativePanel.AlignHorizontalCenterWithPanelProperty, true);
            centerImage.RenderTransform = new CompositeTransform()
            {
                TranslateX = 0
            };
            centerImage.Projection = new PlaneProjection()
            {
                CenterOfRotationX = 1,
                RotationY = 0
            };
            centerImage.Opacity = 1;
            PhotosPanel.Children.Add(centerImage);
            // 左边图片
            var leftImage = GetImage(imagePathList.GetItem(imageCount++));
            leftImage.SetValue(RelativePanel.AlignVerticalCenterWithPanelProperty, true);
            leftImage.SetValue(RelativePanel.AlignHorizontalCenterWithPanelProperty, true);
            leftImage.RenderTransform = new CompositeTransform()
            {
                TranslateX = -imageSize - imageMargin * 2
            };
            leftImage.Projection = new PlaneProjection()
            {
                CenterOfRotationX = 1,
                RotationY = -25
            };
            leftImage.Opacity = 1;
            PhotosPanel.Children.Add(leftImage);
            // 右边图片
            var rightImage = GetImage(imagePathList.GetItem(imageCount++));
            rightImage.SetValue(RelativePanel.AlignVerticalCenterWithPanelProperty, true);
            rightImage.SetValue(RelativePanel.AlignHorizontalCenterWithPanelProperty, true);
            rightImage.RenderTransform = new CompositeTransform()
            {
                TranslateX = imageSize + imageMargin * 2
            };
            rightImage.Projection = new PlaneProjection()
            {
                CenterOfRotationX = 0,
                RotationY = 25
            };
            rightImage.Opacity = 1;
            PhotosPanel.Children.Add(rightImage);
            // 左边边缘图片
            var leftEdgeImage = GetImage(imagePathList.GetItem(imageCount++));
            leftEdgeImage.SetValue(RelativePanel.AlignVerticalCenterWithPanelProperty, true);
            leftEdgeImage.SetValue(RelativePanel.AlignHorizontalCenterWithPanelProperty, true);
            leftEdgeImage.RenderTransform = new CompositeTransform()
            {
                TranslateX = (-imageSize - imageMargin * 2) * 2
            };
            leftEdgeImage.Projection = new PlaneProjection()
            {
                CenterOfRotationX = 1,
                RotationY = -50
            };
            leftEdgeImage.Opacity = 0;
            PhotosPanel.Children.Add(leftEdgeImage);
            // 右边边缘图片
            var rightEdgeImage = GetImage(imagePathList.GetItem(imageCount++));
            rightEdgeImage.SetValue(RelativePanel.AlignVerticalCenterWithPanelProperty, true);
            rightEdgeImage.SetValue(RelativePanel.AlignHorizontalCenterWithPanelProperty, true);
            rightEdgeImage.RenderTransform = new CompositeTransform()
            {
                TranslateX = (imageSize + imageMargin * 2) * 2
            };
            rightEdgeImage.Projection = new PlaneProjection()
            {
                CenterOfRotationX = 0,
                RotationY = 50
            };
            rightEdgeImage.Opacity = 0;
            PhotosPanel.Children.Add(rightEdgeImage);

            imageList.Add(leftEdgeImage);
            imageList.Add(leftImage);
            imageList.Add(centerImage);
            imageList.Add(rightImage);
            imageList.Add(rightEdgeImage);
        }

        /// <summary>
        /// 初始化图片的各种属性大小
        /// </summary>
        private void InitImageAttribute()
        {
            panelWidth = PhotosPanel.ActualWidth;
            imageMargin = 16;
            imageSize = (panelWidth - imageMargin * 8 - 96 * 2) / 3;
            margin = new Thickness()
            {
                Left = imageMargin,
                Right = imageMargin
            };
        }

        /// <summary>
        /// 获得一张显示的图片image对象
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private Image GetImage(string path)
        {
            var image = new Image()
            {
                Width = imageSize,
                Height = imageSize,
                Margin = margin,
                Source = new BitmapImage(new Uri(path))
            };
            return image;
        }

        /// <summary>
        /// 获取水平移动动画
        /// </summary>
        /// <param name="target"></param>
        /// <param name="isLeft"></param>
        /// <returns></returns>
        private DoubleAnimation GetTranslateXAnimation(DependencyObject target, bool isLeft)
        {
            double currentTranslate = ((target as FrameworkElement).RenderTransform as CompositeTransform).TranslateX;
            DoubleAnimation translateXAnimation;
            if (isLeft)
            {
                translateXAnimation = new DoubleAnimation()
                {
                    From = currentTranslate,
                    To = currentTranslate - imageSize - imageMargin * 2,
                    Duration = new Duration(TimeSpan.FromMilliseconds(1000))
                };
            }
            else
            {
                translateXAnimation = new DoubleAnimation()
                {
                    From = currentTranslate,
                    To = currentTranslate + imageSize + imageMargin * 2,
                    Duration = new Duration(TimeSpan.FromMilliseconds(1000))
                };
            }
            Storyboard.SetTarget(translateXAnimation, target);
            Storyboard.SetTargetProperty(translateXAnimation, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");
            return translateXAnimation;
        }

        /// <summary>
        /// 获取旋转动画
        /// </summary>
        /// <param name="target"></param>
        /// <param name="isLeft"></param>
        /// <returns></returns>
        private DoubleAnimation GetRotationYAnimation(DependencyObject target, bool isLeft)
        {
            double currentRotation = ((target as FrameworkElement).Projection as PlaneProjection).RotationY;
            DoubleAnimation rotationYAnimation;
            if (isLeft)
            {
                rotationYAnimation = new DoubleAnimation()
                {
                    From = currentRotation,
                    To = currentRotation - 25,
                    Duration = new Duration(TimeSpan.FromMilliseconds(1000))
                };
            }
            else
            {
                rotationYAnimation = new DoubleAnimation()
                {
                    From = currentRotation,
                    To = currentRotation + 25,
                    Duration = new Duration(TimeSpan.FromMilliseconds(1000))
                };
            }
            Storyboard.SetTargetProperty(rotationYAnimation, "(UIElement.Projection).(PlaneProjection.RotationY)");
            Storyboard.SetTarget(rotationYAnimation, target);
            return rotationYAnimation;
        }

        /// <summary>
        /// 获取渐变动画
        /// </summary>
        /// <param name="target"></param>
        /// <param name="isFade"></param>
        /// <returns></returns>
        private DoubleAnimation GetOpacityAnimation(DependencyObject target, bool isFade)
        {
            DoubleAnimation opacityAnimation;
            if (isFade)
            {
                opacityAnimation = new DoubleAnimation()
                {
                    From = 1,
                    To = 0,
                    Duration = new Duration(TimeSpan.FromMilliseconds(1000))
                };
            }
            else
            {
                opacityAnimation = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = new Duration(TimeSpan.FromMilliseconds(1000))
                };
            }
            Storyboard.SetTargetProperty(opacityAnimation, "(UIElement.Opacity)");
            Storyboard.SetTarget(opacityAnimation, target);
            return opacityAnimation;
        }

        /// <summary>
        /// 左边button的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousButtonHorizontal_Click(object sender, RoutedEventArgs e)
        {
            if (!isAnimateFinished)
            {
                return;
            }
            isAnimateFinished = false;
            
            var centerImage = imageList[2];
            centerImage.Projection = new PlaneProjection()
            {
                CenterOfRotationX = 1,
                RotationY = 0
            };

            Storyboard storyboard = new Storyboard();
            foreach (Image img in imageList)
            {
                storyboard.Children.Add(GetTranslateXAnimation(img, true));
                storyboard.Children.Add(GetRotationYAnimation(img, true));
            }
            storyboard.Children.Add(GetOpacityAnimation(imageList[1], true));
            storyboard.Children.Add(GetOpacityAnimation(imageList[4], false));
            storyboard.Begin();
            storyboard.Completed += (_sender, _e) =>
            {
                PhotosPanel.Children.Remove(imageList[0]);
                PhotosPanel.Children.Remove(imageList[1]);
                imageList.RemoveAt(0);
                imageList.RemoveAt(0);
                // 中心图片
                centerImage = imageList[1];
                centerImage.RenderTransform = new CompositeTransform()
                {
                    TranslateX = 0
                };
                centerImage.Projection = new PlaneProjection()
                {
                    CenterOfRotationX = 1,
                    RotationY = 0
                };
                centerImage.Opacity = 1;
                // 左边图片
                var leftImage = imageList[0];
                leftImage.RenderTransform = new CompositeTransform()
                {
                    TranslateX = -imageSize - imageMargin * 2
                };
                leftImage.Projection = new PlaneProjection()
                {
                    CenterOfRotationX = 1,
                    RotationY = -25
                };
                leftImage.Opacity = 1;
                // 右边图片
                var rightImage = imageList[2];
                rightImage.RenderTransform = new CompositeTransform()
                {
                    TranslateX = imageSize + imageMargin * 2
                };
                rightImage.Projection = new PlaneProjection()
                {
                    CenterOfRotationX = 0,
                    RotationY = 25
                };
                rightImage.Opacity = 1;
                // 左边边缘图片
                var leftEdgeImage = GetImage(imagePathList.GetItem(imageCount++));
                leftEdgeImage.SetValue(RelativePanel.AlignVerticalCenterWithPanelProperty, true);
                leftEdgeImage.SetValue(RelativePanel.AlignHorizontalCenterWithPanelProperty, true);
                leftEdgeImage.RenderTransform = new CompositeTransform()
                {
                    TranslateX = (-imageSize - imageMargin * 2) * 2
                };
                leftEdgeImage.Projection = new PlaneProjection()
                {
                    CenterOfRotationX = 1,
                    RotationY = -50
                };
                leftEdgeImage.Opacity = 0;
                PhotosPanel.Children.Add(leftEdgeImage);
                // 右边边缘图片
                var rightEdgeImage = GetImage(imagePathList.GetItem(imageCount++));
                rightEdgeImage.SetValue(RelativePanel.AlignVerticalCenterWithPanelProperty, true);
                rightEdgeImage.SetValue(RelativePanel.AlignHorizontalCenterWithPanelProperty, true);
                rightEdgeImage.RenderTransform = new CompositeTransform()
                {
                    TranslateX = (imageSize + imageMargin * 2) * 2
                };
                rightEdgeImage.Projection = new PlaneProjection()
                {
                    CenterOfRotationX = 0,
                    RotationY = 50
                };
                rightEdgeImage.Opacity = 0;
                PhotosPanel.Children.Add(rightEdgeImage);
                imageList.Insert(0, leftEdgeImage);
                imageList.Insert(4, rightEdgeImage);

                isAnimateFinished = true;
            };
        }

        /// <summary>
        /// 右边button的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextButtonHorizontal_Click(object sender, RoutedEventArgs e)
        {
            if (!isAnimateFinished)
            {
                return;
            }
            isAnimateFinished = false;

            var centerImage = imageList[2];
            centerImage.Projection = new PlaneProjection()
            {
                CenterOfRotationX = 0,
                RotationY = 0
            };

            Storyboard storyboard = new Storyboard();
            foreach (Image img in imageList)
            {
                storyboard.Children.Add(GetTranslateXAnimation(img, false));
                storyboard.Children.Add(GetRotationYAnimation(img, false));
            }
            storyboard.Children.Add(GetOpacityAnimation(imageList[3], true));
            storyboard.Children.Add(GetOpacityAnimation(imageList[0], false));
            storyboard.Begin();
            storyboard.Completed += (_sender, _e) =>
            {
                PhotosPanel.Children.Remove(imageList[3]);
                PhotosPanel.Children.Remove(imageList[4]);
                imageList.RemoveAt(3);
                imageList.RemoveAt(3);
                // 中心图片
                centerImage = imageList[1];
                centerImage.RenderTransform = new CompositeTransform()
                {
                    TranslateX = 0
                };
                centerImage.Projection = new PlaneProjection()
                {
                    CenterOfRotationX = 1,
                    RotationY = 0
                };
                centerImage.Opacity = 1;
                // 左边图片
                var leftImage = imageList[0];
                leftImage.RenderTransform = new CompositeTransform()
                {
                    TranslateX = -imageSize - imageMargin * 2
                };
                leftImage.Projection = new PlaneProjection()
                {
                    CenterOfRotationX = 1,
                    RotationY = -25
                };
                leftImage.Opacity = 1;
                // 右边图片
                var rightImage = imageList[2];
                rightImage.RenderTransform = new CompositeTransform()
                {
                    TranslateX = imageSize + imageMargin * 2
                };
                rightImage.Projection = new PlaneProjection()
                {
                    CenterOfRotationX = 0,
                    RotationY = 25
                };
                rightImage.Opacity = 1;
                // 左边边缘图片
                var leftEdgeImage = GetImage(imagePathList.GetItem(imageCount++));
                leftEdgeImage.SetValue(RelativePanel.AlignVerticalCenterWithPanelProperty, true);
                leftEdgeImage.SetValue(RelativePanel.AlignHorizontalCenterWithPanelProperty, true);
                leftEdgeImage.RenderTransform = new CompositeTransform()
                {
                    TranslateX = (-imageSize - imageMargin * 2) * 2
                };
                leftEdgeImage.Projection = new PlaneProjection()
                {
                    CenterOfRotationX = 1,
                    RotationY = -50
                };
                leftEdgeImage.Opacity = 0;
                PhotosPanel.Children.Add(leftEdgeImage);
                // 右边边缘图片
                var rightEdgeImage = GetImage(imagePathList.GetItem(imageCount++));
                rightEdgeImage.SetValue(RelativePanel.AlignVerticalCenterWithPanelProperty, true);
                rightEdgeImage.SetValue(RelativePanel.AlignHorizontalCenterWithPanelProperty, true);
                rightEdgeImage.RenderTransform = new CompositeTransform()
                {
                    TranslateX = (imageSize + imageMargin * 2) * 2
                };
                rightEdgeImage.Projection = new PlaneProjection()
                {
                    CenterOfRotationX = 0,
                    RotationY = 50
                };
                rightEdgeImage.Opacity = 0;
                PhotosPanel.Children.Add(rightEdgeImage);
                imageList.Insert(0, leftEdgeImage);
                imageList.Insert(4, rightEdgeImage);

                isAnimateFinished = true;
            };
        }
    }
}
