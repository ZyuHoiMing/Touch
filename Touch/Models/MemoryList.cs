using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage.AccessCache;
using Touch.Data;

namespace Touch.Models
{
    /// <summary>
    ///     回忆列表
    /// </summary>
    public class MemoryList
    {
        /// <summary>
        ///     数据库集合
        /// </summary>
        private readonly DatabaseHelper _databaseHelper;

        /// <summary>
        ///     回忆list
        /// </summary>
        public readonly List<MemoryModel> MemoryModels;

        private MemoryList()
        {
            _databaseHelper = DatabaseHelper.GetInstance();
            MemoryModels = new List<MemoryModel>();
        }

        /// <summary>
        ///     最新key号
        /// </summary>
        public int LastKeyNo => _databaseHelper.MemoryListDatabase.GetLastKeyNo();

        /// <summary>
        ///     异步获取实例
        /// </summary>
        /// <returns>回忆列表</returns>
        public static async Task<MemoryList> GetInstanceAsync()
        {
            var memoryList = new MemoryList();
            // 初始化list
            var query = memoryList._databaseHelper.MemoryListDatabase.GetQuery();
            while (query.Read())
            {
                var memoryKeyNo = query.GetInt32(0);
                // 初始化图片list
                var imageModels = new List<ImageModel>();
                var imageQuery = memoryList._databaseHelper.MemoryImageDatabase.GetQuery(memoryKeyNo);
                while (imageQuery.Read())
                {
                    var imagePath = imageQuery.GetString(2);
                    var accessToken = imageQuery.GetString(3);
                    var imageModel = await ImageModel.GetInstanceAsync(imageQuery.GetInt32(1), imagePath, accessToken);
                    // 查下这个图片还在不在
                    if (imageModel != null)
                    {
                        imageModel.KeyNo = imageQuery.GetInt32(0);
                        imageModels.Add(imageModel);
                    }
                    else
                    {
                        // 从数据库里删掉这个图片
                        memoryList._databaseHelper.ImageDatabase.Delete(imagePath);
                        // 从使用list里删掉这个文件夹
                        StorageApplicationPermissions.FutureAccessList.Remove(accessToken);
                    }
                }
                // 初始化回忆model
                var memoryModel = new MemoryModel
                {
                    KeyNo = memoryKeyNo,
                    MemoryName = query.GetString(1),
                    ImageModels = imageModels
                };
                memoryList.MemoryModels.Add(memoryModel);
            }
            return memoryList;
        }

        /// <summary>
        ///     添加一条记录
        /// </summary>
        /// <param name="memoryModel">回忆</param>
        public void Add(MemoryModel memoryModel)
        {
            if (MemoryModels.Contains(memoryModel))
                return;
            MemoryModels.Add(memoryModel);
            _databaseHelper.MemoryListDatabase.Insert(memoryModel.MemoryName);
            // 在回忆图片数据库里加图片
            foreach (var imageModel in memoryModel.ImageModels)
                _databaseHelper.MemoryImageDatabase.Insert(memoryModel.KeyNo, imageModel.KeyNo);
        }

        /// <summary>
        ///     删除一条记录
        /// </summary>
        /// <param name="memoryModel">回忆</param>
        public void Delete(MemoryModel memoryModel)
        {
            if (!MemoryModels.Contains(memoryModel))
                return;
            MemoryModels.Remove(memoryModel);
            _databaseHelper.MemoryListDatabase.Delete(memoryModel.KeyNo);
            // 在回忆图片数据库里删图片
            _databaseHelper.MemoryImageDatabase.Delete(memoryModel.KeyNo);
        }
    }
}