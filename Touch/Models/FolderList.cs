using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage.AccessCache;
using Touch.Data;

namespace Touch.Models
{
    /// <summary>
    ///     文件夹路径的list
    /// </summary>
    public class FolderList
    {
        /// <summary>
        ///     数据库集合
        /// </summary>
        private readonly DatabaseHelper _databaseHelper;

        /// <summary>
        ///     文件夹路径的list
        /// </summary>
        public readonly List<FolderModel> FolderModels;

        private FolderList()
        {
            _databaseHelper = DatabaseHelper.GetInstance();
            FolderModels = new List<FolderModel>();
        }

        /// <summary>
        ///     异步获取实例
        /// </summary>
        /// <returns></returns>
        public static async Task<FolderList> GetInstanceAsync()
        {
            var folderList = new FolderList();
            // 初始化list
            var query = folderList._databaseHelper.FolderDatabase.GetQuery();
            while (query.Read())
            {
                var folderModel = new FolderModel
                {
                    KeyNo = query.GetInt32(0),
                    FolderPath = query.GetString(1),
                    AccessToken = query.GetString(2)
                };
                // 查下这个文件夹还在不在
                var folder = await folderModel.GetFolder();
                if (folder != null)
                {
                    folderList.FolderModels.Add(folderModel);
                }
                else
                {
                    // 从图片数据库中删掉这个文件夹相关的图片
                    folderList._databaseHelper.ImageDatabase.Delete(folderModel.KeyNo);
                    // 从数据库里删掉这个文件夹
                    folderList._databaseHelper.FolderDatabase.Delete(folderModel.FolderPath);
                    // 从使用list里删掉这个文件夹
                    StorageApplicationPermissions.FutureAccessList.Remove(folderModel.AccessToken);
                }
            }
            return folderList;
        }

        /// <summary>
        ///     添加一条记录
        /// </summary>
        /// <param name="folderModel">文件夹</param>
        public void Add(FolderModel folderModel)
        {
            if (FolderModels.Contains(folderModel))
                return;
            FolderModels.Add(folderModel);
            _databaseHelper.FolderDatabase.Insert(folderModel.FolderPath, folderModel.AccessToken);
        }

        /// <summary>
        ///     删除一条记录
        /// </summary>
        /// <param name="folderModel">文件夹</param>
        public void Delete(FolderModel folderModel)
        {
            if (!FolderModels.Contains(folderModel)) return;
            FolderModels.Remove(folderModel);
            // 从图片数据库中删掉这个文件夹相关的图片
            _databaseHelper.ImageDatabase.Delete(folderModel.KeyNo);
            // 从数据库里删掉这个文件夹
            _databaseHelper.FolderDatabase.Delete(folderModel.FolderPath);
            // 从使用list里删掉这个文件夹
            StorageApplicationPermissions.FutureAccessList.Remove(folderModel.AccessToken);
        }
    }
}