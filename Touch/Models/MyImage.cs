using System;
using Windows.UI.Xaml.Media.Imaging;

namespace Touch.Models
{
    /// <summary>
    ///     图片
    /// </summary>
    public class MyImage:IComparable<MyImage>
    {
        /// <summary>
        ///     图片路径
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        ///     访问权限
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        ///     图片内容
        /// </summary>
        public BitmapImage Bitmap { get; set; }

        /// <summary>
        ///     图片纬度
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        ///     图片经度
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        ///     拍摄日期
        /// </summary>
        public DateTime DateTaken { get; set; }
        /// <summary>
        /// 排序比较函数实现
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(MyImage other)
        {
            if (this.DateTaken < other.DateTaken)
                return -1;
            else if (this.DateTaken == other.DateTaken)
                return 0;
            else return 1;
            throw new NotImplementedException();
        }
    }
}