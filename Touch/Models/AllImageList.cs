using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage.AccessCache;
using Touch.Data;

namespace Touch.Models
{
    /// <summary>
    ///     所有文件夹图片的list
    /// </summary>
    public class AllImageList
    {
        /// <summary>
        ///     所有文件夹的图片list
        /// </summary>
        public readonly List<MyImage> List;

        private AllImageList()
        {
            List = new List<MyImage>();
        }

        /// <summary>
        ///     异步获得实例
        /// </summary>
        /// <returns></returns>
        public static async Task<AllImageList> GetInstanceAsync()
        {
            var allImageList = new AllImageList();
            // 从数据库中得到folder list
            var myFolders = FolderDatabase.GetFolders();
            var folderImageLists = new List<FolderImageList>();
            foreach (var myFolder in myFolders)
            {
                var folder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(myFolder.AccessToken);
                folderImageLists.Add(await FolderImageList.GetInstanceAsync(folder));
            }
            foreach (var folderImageList in folderImageLists)
            foreach (var image in folderImageList.List)
                allImageList.List.Add(image);
            return allImageList;
        }
    }
}