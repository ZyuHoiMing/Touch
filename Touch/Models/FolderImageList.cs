using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Touch.Models
{
    /// <summary>
    ///     一个文件夹内的图片list
    /// </summary>
    public class FolderImageList
    {
        /// <summary>
        ///     一个文件夹内的图片list
        /// </summary>
        public readonly List<MyImage> List;

        private FolderImageList()
        {
            List = new List<MyImage>();
        }

        /// <summary>
        ///     异步获得实例
        /// </summary>
        /// <param name="folder">一定要是有访问权限的文件夹</param>
        /// <returns></returns>
        public static async Task<FolderImageList> GetInstanceAsync(StorageFolder folder)
        {
            var imageList = new FolderImageList();
            var files = await folder.GetFilesAsync();
            foreach (var file in files)
            {
                if (file.FileType != ".jpg" && file.FileType != ".png")
                    continue;
                var bitmap = new BitmapImage();
                using (var stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    bitmap.SetSource(stream);
                    var imageProperties = await file.Properties.GetImagePropertiesAsync();
                    var basicProperties = await file.GetBasicPropertiesAsync();
                    var myImage = new MyImage
                    {
                        ImagePath = file.Path,
                        Bitmap = bitmap,
                        Latitude = imageProperties.Latitude,
                        Longitude = imageProperties.Longitude,
                        // 如果图片的拍摄时间为空，返回文件的修改时间
                        DateTaken = imageProperties.DateTaken.Year <= 1601
                            ? basicProperties.DateModified.LocalDateTime
                            : imageProperties.DateTaken.LocalDateTime
                    };
                    imageList.List.Add(myImage);
                }
            }
            return imageList;
        }
    }
}