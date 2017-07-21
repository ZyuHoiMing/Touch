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
        public List<MyImage> List;

        private FolderImageList()
        {
            List = new List<MyImage>();
        }

        /// <summary>
        /// </summary>
        /// <param name="folder">一定要是有访问权限的文件夹</param>
        /// <returns></returns>
        public static async Task<FolderImageList> GetInstanceAsync(StorageFolder folder)
        {
            var imageList = new FolderImageList();
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