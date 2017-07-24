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
            // 找到全部文件
            var files = await folder.GetFilesAsync();
            foreach (var file in files)
            {
                // 确认后缀名必须是图片
                if (file.FileType != ".jpg" && file.FileType != ".png")
                    continue;
                var bitmap = new BitmapImage();
                using (var stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    // 通过stream流读入图片
                    bitmap.SetSource(stream);
                    // 图片的属性
                    var imageProperties = await file.Properties.GetImagePropertiesAsync();
                    // 文件的基本属性
                    var basicProperties = await file.GetBasicPropertiesAsync();
                    var myImage = new MyImage
                    {
                        ImagePath = file.Path,
                        Bitmap = bitmap,
                        // 纬度
                        Latitude = imageProperties.Latitude,
                        // 经度
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