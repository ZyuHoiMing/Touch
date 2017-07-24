using System.Collections.Generic;
using Touch.Data;

namespace Touch.Models
{
    /// <summary>
    ///     回忆的列表
    /// </summary>
    public class MemoryList
    {
        /// <summary>
        ///     回忆list
        /// </summary>
        public readonly List<MyMemory> List;

        public MemoryList()
        {
            List = MemoryListDatabase.GetMemoryList() as List<MyMemory>;
            if (List == null) return;
            foreach (var myMemory in List)
                myMemory.Images = MemoryImageDatabase.GetImageList(myMemory.KeyNo) as List<MyImage>;
        }

        /// <summary>
        ///     添加一条记录
        /// </summary>
        /// <param name="myMemory"></param>
        public void Add(MyMemory myMemory)
        {
            if (List.Contains(myMemory)) return;
            List.Add(myMemory);
            MemoryListDatabase.Insert(myMemory.MemoryName);
            var keyNo = MemoryListDatabase.GetLastKeyNo();
            foreach (var myImage in myMemory.Images)
                MemoryImageDatabase.Insert(keyNo, myImage.ImagePath, myImage.AccessToken);
        }

        /// <summary>
        ///     删除一条记录
        /// </summary>
        /// <param name="myMemory"></param>
        public void Delete(MyMemory myMemory)
        {
            if (!List.Contains(myMemory)) return;
            List.Remove(myMemory);
            MemoryListDatabase.Delete(myMemory.KeyNo);
            MemoryImageDatabase.Delete(myMemory.KeyNo);
        }
    }
}