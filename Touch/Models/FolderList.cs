using System.Collections.Generic;
using Touch.Data;

namespace Touch.Models
{
    /// <summary>
    ///     文件夹路径的list
    /// </summary>
    public class FolderList
    {
        /// <summary>
        ///     文件夹路径的list
        /// </summary>
        public readonly List<MyFolder> List;

        public FolderList()
        {
            List = FolderDatabase.GetFolders() as List<MyFolder>;
        }

        /// <summary>
        ///     添加一条记录
        /// </summary>
        /// <param name="folder">文件夹</param>
        public void Add(MyFolder folder)
        {
            if (List.Contains(folder)) return;
            List.Add(folder);
            FolderDatabase.Insert(folder.FolderPath);
        }

        /// <summary>
        ///     删除一条记录
        /// </summary>
        /// <param name="folder">文件夹</param>
        public void Delete(MyFolder folder)
        {
            if (!List.Contains(folder)) return;
            List.Remove(folder);
            FolderDatabase.Delete(folder.FolderPath);
        }
    }
}