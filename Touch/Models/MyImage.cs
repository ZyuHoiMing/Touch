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
    }
}