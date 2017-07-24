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
        public static async Task<FolderImageList> GetInstanceAsync(StorageFolder folder)//文件夹
        {
            var imageList = new FolderImageList();
            var files = await folder.GetFilesAsync();//找到全部文件
            foreach (var file in files)
            {
                if (file.FileType != ".jpg" && file.FileType != ".png")//确认后缀名必须是图片
                    continue;
                var bitmap = new BitmapImage();
                using (var stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    bitmap.SetSource(stream);//通过stream流读入图片
                    var imageProperties = await file.Properties.GetImagePropertiesAsync();//图片的属性
                    var basicProperties = await file.GetBasicPropertiesAsync();//文件的基本属性
                    var myImage = new MyImage
                    {
                        ImagePath = file.Path,
                        Bitmap = bitmap,
                        Latitude = imageProperties.Latitude,//纬度
                        Longitude = imageProperties.Longitude,//经度
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