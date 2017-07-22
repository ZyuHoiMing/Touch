using System;
using Windows.UI.Xaml.Media.Imaging;

namespace Touch.Models
{
    public class MyImage
    {
        /// <summary>
        ///     图片路径
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        ///     图片内容
        /// </summary>
        public BitmapImage Bitmap { get; set; }

        /// <summary>
        ///     图片纬度
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        ///     图片精度
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        ///     拍摄日期
        /// </summary>
        public DateTime DateTaken { get; set; }
    }
}