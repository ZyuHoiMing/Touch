using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;
using Touch.Models;

namespace Touch.ViewModels
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    /// <summary>
    ///     图片ViewModel
    /// </summary>
    public class ImageViewModel : NotificationBase
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        /// <summary>
        ///     图片文件
        /// </summary>
        private StorageFile _imageFile;

        /// <summary>
        ///     图片model
        /// </summary>
        private ImageModel _imageModel;

        private ImageViewModel()
        {
        }

        /// <summary>
        ///     图片编号
        /// </summary>
        public int KeyNo => _imageModel.KeyNo;

        /// <summary>
        ///     所属文件夹编号
        /// </summary>
        public int FolderKeyNo => _imageModel.FolderKeyNo;

        /// <summary>
        ///     图片路径
        /// </summary>
        public string ImagePath => _imageModel.ImagePath;

        /// <summary>
        ///     访问权限
        /// </summary>
        public string AccessToken => _imageModel.AccessToken;

        /// <summary>
        ///     图片宽度
        /// </summary>
        public uint Width => _imageModel.Width;

        /// <summary>
        ///     图片长度
        /// </summary>
        public uint Height => _imageModel.Height;

        /// <summary>
        ///     图片纬度
        /// </summary>
        public double? Latitude => _imageModel.Latitude;

        /// <summary>
        ///     图片经度
        /// </summary>
        public double? Longitude => _imageModel.Longitude;

        /// <summary>
        ///     拍摄日期
        /// </summary>
        public DateTime DateTaken => _imageModel.DateTaken;

        /// <summary>
        ///     只有年和月的拍摄日期
        /// </summary>
        public string MonthYearDate => DateTaken.Year + " - " + DateTaken.Month;

        /// <summary>
        ///     异步获取实例
        /// </summary>
        /// <returns></returns>
        public static async Task<ImageViewModel> GetInstanceAsync(int folderKeyNo, string imagePath, string accessToken)
        {
            var imageViewModel = new ImageViewModel
            {
                _imageModel = await ImageModel.GetInstanceAsync(folderKeyNo, imagePath, accessToken)
            };
            imageViewModel._imageFile = imageViewModel._imageModel.ImageFile;
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
        public async Task<BitmapImage> GetThumbnailImageAsync(uint requiredSize)
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
        public async Task<BitmapImage> GetOriginalImageAsync()
        {
            var bitmap = new BitmapImage();
            using (var stream = await _imageFile.OpenAsync(FileAccessMode.Read))
            {
                bitmap.SetSource(stream);
            }
            return bitmap;
        }
    }
}