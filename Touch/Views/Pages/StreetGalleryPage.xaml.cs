using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Touch.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
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
        /// 中心图片在循环队列中的位置
        /// </summary>
        private int centerImageNum = 0;
        private bool isAnimateFinished = true;
        private enum ImageIndex { LeftEdge, Left, Center, Right, RightEdge };

        public StreetGalleryPage()
        {
            this.InitializeComponent();

            // TODO image应该从data里拿
            List<string> paths = new List<string>
            {
                "ms-appx:///Assets/pic1.jpg",
                "ms-appx:///Assets/pic2.jpg",
                "ms-appx:///Assets/pic3.jpg",
                "ms-appx:///Assets/pic4.jpg",
                "ms-appx:///Assets/pic5.jpg",
                "ms-appx:///Assets/pic6.jpg"
            };
            imagePathList = new ImageRecycleList(paths);
            imageList = new List<Image>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitImageAttribute();

            // 中心图片
            var centerImage = GetImage(imagePathList.GetItem(centerImageNum), ImageIndex.Center);
            PhotosPanel.Children.Add(centerImage);
            // 左边图片
            var leftImage = GetImage(imagePathList.GetItem(centerImageNum - 1), ImageIndex.Left);
            PhotosPanel.Children.Add(leftImage);
            // 右边图片
            var rightImage = GetImage(imagePathList.GetItem(centerImageNum + 1), ImageIndex.Right);
            PhotosPanel.Children.Add(rightImage);
            // 左边边缘图片
            var leftEdgeImage = GetImage(imagePathList.GetItem(centerImageNum - 2), ImageIndex.LeftEdge);
            PhotosPanel.Children.Add(leftEdgeImage);
            // 右边边缘图片
            var rightEdgeImage = GetImage(imagePathList.GetItem(centerImageNum + 2), ImageIndex.RightEdge);
            PhotosPanel.Children.Add(rightEdgeImage);
            // 全部添加到图片显示list里
            imageList.Add(leftEdgeImage);
            imageList.Add(leftImage);
            imageList.Add(centerImage);
            imageList.Add(rightImage);
            imageList.Add(rightEdgeImage);
            // 设置background
            BackgroundUpImage.Source = new BitmapImage(new Uri(imagePathList.GetItem(centerImageNum)));
            BackgroundDownImage.Source = new BitmapImage(new Uri(imagePathList.GetItem(centerImageNum)));
        }

        /// <summary>
        /// 初始化图片的各种属性大小
        /// </summary>
        private void InitImageAttribute()
        {
            panelWidth = PhotosPanel.ActualWidth;
            imageMargin = 16;
            imageSize = (panelWidth - imageMargin * 8 - 48 * 2) / 3;
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
        private Image GetImage(string path, ImageIndex index)
        {
            var image = new Image()
            {
                Width = imageSize,
                Height = imageSize,
                Source = new BitmapImage(new Uri(path)),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            switch (index)
            {
                // 中心图片
                case ImageIndex.Center:
                    image.RenderTransform = new CompositeTransform()
                    {
                        TranslateX = 0
                    };
                    break;
                // 左边图片
                case ImageIndex.Left:
                    image.RenderTransform = new CompositeTransform()
                    {
                        TranslateX = -imageSize - imageMargin * 2
                    };
                    image.Projection = new PlaneProjection()
                    {
                        CenterOfRotationX = 1,
                        RotationY = -25
                    };
                    break;
                // 右边图片
                case ImageIndex.Right:
                    image.RenderTransform = new CompositeTransform()
                    {
                        TranslateX = imageSize + imageMargin * 2
                    };
                    image.Projection = new PlaneProjection()
                    {
                        CenterOfRotationX = 0,
                        RotationY = 25
                    };
                    break;
                // 左边边缘图片
                case ImageIndex.LeftEdge:
                    image.RenderTransform = new CompositeTransform()
                    {
                        TranslateX = (-imageSize - imageMargin * 2) * 2
                    };
                    image.Projection = new PlaneProjection()
                    {
                        CenterOfRotationX = 1,
                        RotationY = -50
                    };
                    image.Opacity = 0;
                    break;
                // 右边边缘图片
                case ImageIndex.RightEdge:
                    image.RenderTransform = new CompositeTransform()
                    {
                        TranslateX = (imageSize + imageMargin * 2) * 2
                    };
                    image.Projection = new PlaneProjection()
                    {
                        CenterOfRotationX = 0,
                        RotationY = 50
                    };
                    image.Opacity = 0;
                    break;
                default:
                    break;
            }
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
        /// 左button的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousButtonHorizontal_Click(object sender, RoutedEventArgs e)
        {
            // 如果当前在播放动画，不响应点击事件
            if (!isAnimateFinished)
            {
                return;
            }
            isAnimateFinished = false;
            // 设置背景图下层图片
            BackgroundDownImage.Source = new BitmapImage(new Uri(imagePathList.GetItem(centerImageNum + 1)));
            // 给中心图片设置旋转轴
            var centerImage = imageList[2];
            centerImage.Projection = new PlaneProjection()
            {
                CenterOfRotationX = 1,
                RotationY = 0
            };
            // 准备动画并播放
            Storyboard storyboard = new Storyboard();
            foreach (Image img in imageList)
            {
                storyboard.Children.Add(GetTranslateXAnimation(img, true));
                storyboard.Children.Add(GetRotationYAnimation(img, true));
            }
            storyboard.Children.Add(GetOpacityAnimation(imageList[1], true));
            storyboard.Children.Add(GetOpacityAnimation(imageList[4], false));
            // 背景切换模糊
            storyboard.Children.Add(GetOpacityAnimation(BackgroundUpImage, true));
            storyboard.Begin();
            // 重新准备下一波的图片
            storyboard.Completed += (_sender, _e) =>
            {
                // 原来的左图和左边缘图都从布局中剔除
                PhotosPanel.Children.Remove(imageList[0]);
                PhotosPanel.Children.Remove(imageList[1]);

                centerImageNum++;
                // 中心图片
                centerImage = imageList[3];
                // 左边图片
                var leftImage = imageList[2];
                // 右边图片
                var rightImage = imageList[4];
                // 左边边缘图片
                var leftEdgeImage = GetImage(imagePathList.GetItem(centerImageNum - 2), ImageIndex.LeftEdge);
                // 右边边缘图片
                var rightEdgeImage = GetImage(imagePathList.GetItem(centerImageNum + 2), ImageIndex.RightEdge);

                PhotosPanel.Children.Add(rightEdgeImage);
                PhotosPanel.Children.Add(leftEdgeImage);

                imageList.Clear();
                imageList.Add(leftEdgeImage);
                imageList.Add(leftImage);
                imageList.Add(centerImage);
                imageList.Add(rightImage);
                imageList.Add(rightEdgeImage);
                // 设置背景图下层图片
                BackgroundUpImage.Source = new BitmapImage(new Uri(imagePathList.GetItem(centerImageNum)));
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
            // 如果当前在播放动画，不响应点击事件
            if (!isAnimateFinished)
            {
                return;
            }
            isAnimateFinished = false;
            // 设置背景图下层图片
            BackgroundDownImage.Source = new BitmapImage(new Uri(imagePathList.GetItem(centerImageNum - 1)));
            // 给中心图片设置旋转轴
            var centerImage = imageList[2];
            centerImage.Projection = new PlaneProjection()
            {
                CenterOfRotationX = 0,
                RotationY = 0
            };
            // 准备动画并播放
            Storyboard storyboard = new Storyboard();
            foreach (Image img in imageList)
            {
                storyboard.Children.Add(GetTranslateXAnimation(img, false));
                storyboard.Children.Add(GetRotationYAnimation(img, false));
            }
            storyboard.Children.Add(GetOpacityAnimation(imageList[3], true));
            storyboard.Children.Add(GetOpacityAnimation(imageList[0], false));
            // 背景切换模糊
            storyboard.Children.Add(GetOpacityAnimation(BackgroundUpImage, true));
            storyboard.Begin();
            // 重新准备下一波的图片
            storyboard.Completed += (_sender, _e) =>
            {
                // 原来的左图和左边缘图都从布局中剔除
                PhotosPanel.Children.Remove(imageList[3]);
                PhotosPanel.Children.Remove(imageList[4]);

                centerImageNum--;
                // 中心图片
                centerImage = imageList[1];
                // 左边图片
                var leftImage = imageList[0];
                // 右边图片
                var rightImage = imageList[2];
                // 左边边缘图片
                var leftEdgeImage = GetImage(imagePathList.GetItem(centerImageNum - 2), ImageIndex.LeftEdge);
                // 右边边缘图片
                var rightEdgeImage = GetImage(imagePathList.GetItem(centerImageNum + 2), ImageIndex.RightEdge);

                PhotosPanel.Children.Add(rightEdgeImage);
                PhotosPanel.Children.Add(leftEdgeImage);

                imageList.Clear();
                imageList.Add(leftEdgeImage);
                imageList.Add(leftImage);
                imageList.Add(centerImage);
                imageList.Add(rightImage);
                imageList.Add(rightEdgeImage);
                // 设置背景图下层图片
                BackgroundUpImage.Source = new BitmapImage(new Uri(imagePathList.GetItem(centerImageNum)));
                isAnimateFinished = true;
            };
        }
    }
}
