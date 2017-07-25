using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage.AccessCache;
using Touch.Data;

namespace Touch.Models
{
    /// <summary>
    ///     一个文件夹内的图片list
    /// </summary>
    public class ImageFolderList
    {
        /// <summary>
        ///     数据库集合
        /// </summary>
        private readonly DatabaseHelper _databaseHelper;

        /// <summary>
        ///     文件夹
        /// </summary>
        private readonly FolderModel _folderModel;

        /// <summary>
        ///     一个文件夹内的图片list
        /// </summary>
        public readonly List<ImageModel> ImageModels;

        private ImageFolderList(FolderModel folderModel)
        {
            _folderModel = folderModel;
            _databaseHelper = DatabaseHelper.GetInstance();
            ImageModels = new List<ImageModel>();
        }

        /// <summary>
        ///     异步获得实例
        /// </summary>
        /// <returns></returns>
        public static async Task<ImageFolderList> GetInstanceAsync(FolderModel folderModel)
        {
            var imageFolderList = new ImageFolderList(folderModel);
            // 初始化list
            var query = imageFolderList._databaseHelper.ImageDatabase.GetQuery(folderModel.KeyNo);
            while (query.Read())
            {
                var imagePath = query.GetString(2);
                var accessToken = query.GetString(3);
                var imageModel =
                    await ImageModel.GetInstanceAsync(query.GetInt32(1), imagePath, accessToken);
                // 查下这个图片还在不在
                if (imageModel != null)
                {
                    imageModel.KeyNo = query.GetInt32(0);
                    imageFolderList.ImageModels.Add(imageModel);
                }
                else
                {
                    // 从数据库里删掉这个图片
                    imageFolderList._databaseHelper.ImageDatabase.Delete(imagePath);
                    // 从使用list里删掉这个文件夹
                    StorageApplicationPermissions.FutureAccessList.Remove(accessToken);
                }
            }
            return imageFolderList;
        }

        /// <summary>
        ///     刷新list
        /// </summary>
        /// <returns></returns>
        public async Task RefreshAsync()
        {
            var folder = await _folderModel.GetFolder();
            // TODO 这里文件夹可能为空
            // 找到全部文件
            var files = await folder.GetFilesAsync();
            foreach (var file in files)
            {
                // 确认后缀名必须是图片
                if (file.FileType != ".jpg" && file.FileType != ".png" && file.FileType != ".jpeg")
                    continue;
                var accessToken = StorageApplicationPermissions.FutureAccessList.Add(file);
                var imageModel = await ImageModel.GetInstanceAsync(_folderModel.KeyNo, file.Path, accessToken);
                Add(imageModel);
            }
        }

        /// <summary>
        ///     添加一条记录
        /// </summary>
        /// <param name="imageModel">图片</param>
        private void Add(ImageModel imageModel)
        {
            if (ImageModels.Contains(imageModel))
                return;
            _databaseHelper.ImageDatabase.Insert(imageModel.FolderKeyNo, imageModel.ImagePath, imageModel.AccessToken);
            ImageModels.Add(imageModel);
        }
    }
}