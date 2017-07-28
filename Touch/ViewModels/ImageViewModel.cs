using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;
using Touch.Models;
using Windows.Globalization.DateTimeFormatting;

namespace Touch.ViewModels
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    /// <summary>
    ///     图片ViewModel
    /// </summary>
    public class ImageViewModel : NotificationBase, IComparable<ImageViewModel>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        /// <summary>
        ///     图片文件
        /// </summary>
        private StorageFile _imageFile;

        private ImageViewModel()
        {
        }

        /// <summary>
        ///     图片model
        /// </summary>
        public ImageModel ImageModel { get; private set; }

        /// <summary>
        ///     图片编号
        /// </summary>
        public int KeyNo => ImageModel.KeyNo;

        /// <summary>
        ///     所属文件夹编号
        /// </summary>
        public int FolderKeyNo => ImageModel.FolderKeyNo;

        /// <summary>
        ///     图片路径
        /// </summary>
        public string ImagePath => ImageModel.ImagePath;

        /// <summary>
        ///     访问权限
        /// </summary>
        public string AccessToken => ImageModel.AccessToken;

        /// <summary>
        ///     图片宽度
        /// </summary>
        public uint Width => ImageModel.Width;

        /// <summary>
        ///     图片长度
        /// </summary>
        public uint Height => ImageModel.Height;

        /// <summary>
        ///     图片纬度
        /// </summary>
        public double? Latitude => ImageModel.Latitude;

        /// <summary>
        ///     图片经度
        /// </summary>
        public double? Longitude => ImageModel.Longitude;

        /// <summary>
        ///     拍摄日期
        /// </summary>
        public DateTime DateTaken => ImageModel.DateTaken;

        /// <summary>
        ///     只有年和月的拍摄日期
        /// </summary>
        public MonthYearDateTime MonthYearDate => new MonthYearDateTime(DateTaken);

        /// <summary>
        ///     缩略图
        /// </summary>
        public BitmapImage ThumbnailImage { get; private set; }

        /// <summary>
        ///     排序比较函数实现
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(ImageViewModel other)
        {
            if (DateTaken < other.DateTaken)
                return -1;
            return DateTaken == other.DateTaken ? 0 : 1;
        }

        /// <summary>
        ///     原始图片
        /// </summary>
        //public BitmapImage OriginalImage { get; private set; }
        /// <summary>
        ///     异步获取实例
        /// </summary>
        /// <returns></returns>
        public static async Task<ImageViewModel> GetInstanceAsync(ImageModel imageModel)
        {
            var imageViewModel = new ImageViewModel
            {
                ImageModel = imageModel,
                _imageFile = imageModel.ImageFile
            };
            imageViewModel.ThumbnailImage = await imageViewModel.GetThumbnailImageAsync(400);
            //imageViewModel.OriginalImage = await imageViewModel.GetOriginalImageAsync();
            return imageViewModel;
        }

#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var o = obj as ImageViewModel;
            return o != null && o.ImagePath == ImagePath;
        }

        /// <summary>
        ///     获得缩略图
        /// </summary>
        /// <param name="requiredSize">需要的图片大小（最长的边）</param>
        /// <returns>缩略图</returns>
        private async Task<BitmapImage> GetThumbnailImageAsync(uint requiredSize)
        {
            var fileThumbnail = await _imageFile.GetThumbnailAsync(ThumbnailMode.SingleItem, requiredSize);
            var bitmap = new BitmapImage();
            bitmap.SetSource(fileThumbnail);
            return bitmap;
        }

        /// <summary>
        ///     获得原始图片
        /// </summary>
        /// <returns>原始图片</returns>
        private async Task<BitmapImage> GetOriginalImageAsync()
        {
            var bitmap = new BitmapImage();
            using (var stream = await _imageFile.OpenAsync(FileAccessMode.Read))
            {
                await bitmap.SetSourceAsync(stream);
            }
            return bitmap;
        }
    }
}