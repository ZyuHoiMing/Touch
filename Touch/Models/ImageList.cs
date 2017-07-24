﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Xaml.Media.Imaging;

namespace Touch.Models
{
    /// <summary>
    ///     图片list
    /// </summary>
    public class ImageList
    {
        /// <summary>
        ///     图片list
        /// </summary>
        public readonly List<MyImage> List;

        private ImageList()
        {
            List = new List<MyImage>();
        }

        /// <summary>
        ///     异步获得实例，一个文件夹内的图片list
        /// </summary>
        /// <param name="folder">一定要是有访问权限的文件夹</param>
        /// <returns></returns>
        public static async Task<ImageList> GetInstanceAsync(StorageFolder folder)
        {
            var files = await folder.GetFilesAsync();
            var imageList = await GetInstanceAsync(files);
            return imageList;
        }

        /// <summary>
        ///     异步获得实例，一个文件夹内的图片list
        /// </summary>
        /// <param name="files">一定要是有访问权限的文件list</param>
        /// <returns></returns>
        public static async Task<ImageList> GetInstanceAsync(IEnumerable<StorageFile> files)
        {
            var imageList = new ImageList();
            foreach (var file in files)
            {
                if (file.FileType != ".jpg" && file.FileType != ".png")
                    continue;
                var myImage = await GetImageAsync(file);
                if (myImage == null)
                    continue;
                imageList.List.Add(myImage);
            }
            return imageList;
        }

        /// <summary>
        ///     通过文件获得MyImage，可能为null
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static async Task<MyImage> GetImageAsync(StorageFile file)
        {
            MyImage myImage;
            using (var stream = await file.OpenAsync(FileAccessMode.Read))
            {
                var bitmap = new BitmapImage();
                bitmap.SetSource(stream);
                var imageProperties = await file.Properties.GetImagePropertiesAsync();
                var basicProperties = await file.GetBasicPropertiesAsync();
                var accessToken = StorageApplicationPermissions.FutureAccessList.Add(file);
                myImage = new MyImage
                {
                    ImagePath = file.Path,
                    Bitmap = bitmap,
                    Latitude = imageProperties.Latitude,
                    Longitude = imageProperties.Longitude,
                    // 如果图片的拍摄时间为空，返回文件的修改时间
                    DateTaken = imageProperties.DateTaken.Year <= 1601
                        ? basicProperties.DateModified.LocalDateTime
                        : imageProperties.DateTaken.LocalDateTime,
                    AccessToken = accessToken
                };
            }
            return myImage;
        }
    }
}