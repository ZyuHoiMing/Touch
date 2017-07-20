using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Touch.Models
{
    public class ImageList
    {
        /// <summary>
        ///     一个文件夹内的图片list
        /// </summary>
        public List<MyImage> List;

        private ImageList()
        {
            List = new List<MyImage>();
        }

        public static async Task<ImageList> GetInstanceAsync(StorageFolder folder)
        {
            var imageList = new ImageList();
            var files = await folder.GetFilesAsync();
            foreach (var file in files)
            {
                var bitmap = new BitmapImage();
                using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    bitmap.SetSource(stream);
                    imageList.List.Add(new MyImage {Bitmap = bitmap});
                }
            }
            return imageList;
        }
    }
}