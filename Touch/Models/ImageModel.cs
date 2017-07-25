using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;

namespace Touch.Models
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    /// <summary>
    ///     图片
    /// </summary>
    public class ImageModel
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        private ImageModel(int folderKeyNo, string imagePath, string accessToken)
        {
            FolderKeyNo = folderKeyNo;
            ImagePath = imagePath;
            AccessToken = accessToken;
        }

        /// <summary>
        ///     图片编号
        /// </summary>
        public int KeyNo { get; set; }

        /// <summary>
        ///     所属文件夹编号
        /// </summary>
        public int FolderKeyNo { get; }

        /// <summary>
        ///     图片路径
        /// </summary>
        public string ImagePath { get; }

        /// <summary>
        ///     访问权限
        /// </summary>
        public string AccessToken { get; }

        /// <summary>
        ///     图片宽度
        /// </summary>
        public uint Width { get; private set; }

        /// <summary>
        ///     图片长度
        /// </summary>
        public uint Height { get; private set; }

        /// <summary>
        ///     图片纬度
        /// </summary>
        public double? Latitude { get; private set; }

        /// <summary>
        ///     图片经度
        /// </summary>
        public double? Longitude { get; private set; }

        /// <summary>
        ///     拍摄日期
        /// </summary>
        public DateTime DateTaken { get; private set; }

        /// <summary>
        ///     图片文件
        /// </summary>
        public StorageFile ImageFile { get; private set; }

        /// <summary>
        ///     获得图片实例
        /// </summary>
        /// <param name="folderKeyNo">文件夹号</param>
        /// <param name="imagePath">图片路径</param>
        /// <param name="accessToken">访问权限</param>
        /// <returns>图片，如果不存在就返回null</returns>
        public static async Task<ImageModel> GetInstanceAsync(int folderKeyNo, string imagePath, string accessToken)
        {
            var myImage = new ImageModel(folderKeyNo, imagePath, accessToken)
            {
                ImageFile = await GetImageFile(accessToken)
            };
            if (myImage.ImageFile == null)
                return null;
            var imageProperties = await myImage.ImageFile.Properties.GetImagePropertiesAsync();
            var basicProperties = await myImage.ImageFile.GetBasicPropertiesAsync();
            myImage.Width = imageProperties.Width;
            myImage.Height = myImage.Height;
            myImage.Latitude = imageProperties.Latitude;
            myImage.Longitude = imageProperties.Longitude;
            // 如果图片的拍摄时间为空，返回文件的修改时间
            myImage.DateTaken = imageProperties.DateTaken.Year <= 1601
                ? basicProperties.DateModified.LocalDateTime
                : imageProperties.DateTaken.LocalDateTime;
            return myImage;
        }

#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var o = obj as ImageModel;
            return o != null && o.ImagePath == ImagePath;
        }

        /// <summary>
        ///     查下这个图片还在不在
        /// </summary>
        /// <param name="accessToken">访问权限</param>
        /// <returns>存在返回file，不存在返回null</returns>
        public static async Task<StorageFile> GetImageFile(string accessToken)
        {
            try
            {
                return await StorageApplicationPermissions.FutureAccessList.GetFileAsync(accessToken);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }
    }
}