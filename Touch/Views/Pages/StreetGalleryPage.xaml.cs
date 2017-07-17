using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        Thickness margin;

        public StreetGalleryPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitImageAttribute();

            // 中心图片
            var centerImage = GetImage("ms-appx:///Assets/pic1_square.jpg");
            centerImage.SetValue(RelativePanel.AlignVerticalCenterWithPanelProperty, true);
            centerImage.SetValue(RelativePanel.AlignHorizontalCenterWithPanelProperty, true);
            centerImage.RenderTransform = new CompositeTransform();
            centerImage.Projection = new PlaneProjection()
            {
                CenterOfRotationX = 1,
                RotationY = 0
            };
            PhotosPanel.Children.Add(centerImage);
            // 左边图片
            var leftImage = GetImage("ms-appx:///Assets/pic1_square.jpg");
            leftImage.SetValue(RelativePanel.LeftOfProperty, centerImage);
            leftImage.SetValue(RelativePanel.AlignTopWithProperty, centerImage);
            leftImage.RenderTransform = new CompositeTransform();
            leftImage.Projection = new PlaneProjection()
            {
                CenterOfRotationX = 1,
                RotationY = -25
            };
            PhotosPanel.Children.Add(leftImage);
            //// 右边图片
            //var rightImage = new Image()
            //{
            //    Width = imageWidth,
            //    Margin = margin,
            //    Source = new BitmapImage(new Uri("ms-appx:///Assets/pic1_square.jpg"))
            //};
            //rightImage.SetValue(RelativePanel.RightOfProperty, centerImage);
            //rightImage.SetValue(RelativePanel.AlignTopWithProperty, centerImage);
            //PhotosPanel.Children.Add(rightImage);
            //// 左边边缘图片
            //var leftEdgeImage = new Image()
            //{
            //    Width = imageWidth,
            //    Margin = margin,
            //    Source = new BitmapImage(new Uri("ms-appx:///Assets/pic1_square.jpg"))
            //};
            //leftEdgeImage.SetValue(RelativePanel.LeftOfProperty, leftImage);
            //leftEdgeImage.SetValue(RelativePanel.AlignTopWithProperty, leftImage);
            //PhotosPanel.Children.Add(leftEdgeImage);
            //// 右边边缘图片
            //var rightEdgeImage = new Image()
            //{
            //    Width = imageWidth,
            //    Margin = margin,
            //    Source = new BitmapImage(new Uri("ms-appx:///Assets/pic1_square.jpg"))
            //};
            //rightEdgeImage.SetValue(RelativePanel.RightOfProperty, rightImage);
            //rightEdgeImage.SetValue(RelativePanel.AlignTopWithProperty, rightImage);
            //PhotosPanel.Children.Add(rightEdgeImage);

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(GetTranslateXAnimation(centerImage));
            storyboard.Children.Add(GetRotationYAnimation(centerImage));
            storyboard.Children.Add(GetTranslateXAnimation(leftImage));
            storyboard.Children.Add(GetRotationYAnimation(leftImage));
            storyboard.Begin();
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
        /// <returns></returns>
        private DoubleAnimation GetTranslateXAnimation(DependencyObject target)
        {
            DoubleAnimation translateXAnimation = new DoubleAnimation()
            {
                From = 0,
                To = -imageSize - imageMargin * 2,
                Duration = new Duration(TimeSpan.FromMilliseconds(1000))
            };
            Storyboard.SetTarget(translateXAnimation, target);
            Storyboard.SetTargetProperty(translateXAnimation, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");
            return translateXAnimation;
        }

        /// <summary>
        /// 获取旋转动画
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private DoubleAnimation GetRotationYAnimation(DependencyObject target)
        {
            DoubleAnimation rotationYAnimation = new DoubleAnimation()
            {
                From = 0,
                To = -25,
                Duration = new Duration(TimeSpan.FromMilliseconds(1000))
            };
            Storyboard.SetTargetProperty(rotationYAnimation, "(UIElement.Projection).(PlaneProjection.RotationY)");
            Storyboard.SetTarget(rotationYAnimation, target);
            return rotationYAnimation;
        }
    }
}
