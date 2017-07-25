using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;

namespace Touch.Models
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    /// <summary>
    ///     文件夹
    /// </summary>
    public class FolderModel
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        /// <summary>
        ///     文件夹编号
        /// </summary>
        public int KeyNo { get; set; }

        /// <summary>
        ///     文件夹路径
        /// </summary>
        public string FolderPath { get; set; }

        /// <summary>
        ///     访问权限
        /// </summary>
        public string AccessToken { get; set; }

#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var o = obj as FolderModel;
            return o != null && o.FolderPath == FolderPath;
        }

        /// <summary>
        ///     获取文件夹
        /// </summary>
        /// <returns>存在返回Folder，不存在返回null</returns>
        public async Task<StorageFolder> GetFolder()
        {
            try
            {
                return await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(AccessToken);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }
    }
}