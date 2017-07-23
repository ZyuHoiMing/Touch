using Microsoft.VisualStudio.TestTools.UnitTesting;
using Touch.Data;

namespace Touch.UnitTestProject.Data
{
    [TestClass]
    public class MemoryImageDatabaseUnitTest
    {
        /// <summary>
        ///     插入并读出数据
        /// </summary>
        [TestMethod]
        public void InsertAndGetFoldersTest()
        {
            DatabaseHelper.InitDb();
            // 先创建主表
            MemoryListDatabase.Drop();
            MemoryListDatabase.Create();
            for (var i = 1; i <= 3; i++)
                MemoryListDatabase.Insert("test_data_" + i);
            // 再创建外键表
            MemoryImageDatabase.Drop();
            MemoryImageDatabase.Create();
            for (var i = 1; i <= 3; i++)
            for (var j = 1; j <= 3; j++)
                MemoryImageDatabase.Insert(i, "test_data_" + i + "_" + j, "");
            // 读，是否相等
            for (var i = 1; i <= 3; i++)
                using (var imageListEnumerator = MemoryImageDatabase.GetImageList(i).GetEnumerator())
                {
                    for (var j = 1; j <= 3; j++)
                    {
                        if (!imageListEnumerator.MoveNext())
                            return;
                        Assert.AreEqual("test_data_" + i + "_" + j, imageListEnumerator.Current.ImagePath);
                    }
                }
        }

        /// <summary>
        ///     通过外键删除并读出数据
        /// </summary>
        [TestMethod]
        public void DeleteAndGetFoldersTest()
        {
            // 先创建主表
            MemoryListDatabase.Drop();
            MemoryListDatabase.Create();
            for (var i = 1; i <= 5; i++)
                MemoryListDatabase.Insert("test_data_" + i);
            // 再创建外键表
            MemoryImageDatabase.Drop();
            MemoryImageDatabase.Create();
            for (var i = 1; i <= 5; i++)
            for (var j = 1; j <= 5; j++)
                MemoryImageDatabase.Insert(i, "test_data_" + i + "_" + j, "");
            for (var i = 1; i <= 5; i++)
                using (var imageListEnumerator = MemoryImageDatabase.GetImageList(i).GetEnumerator())
                {
                    for (var j = 1; j <= 5; j++)
                    {
                        if (!imageListEnumerator.MoveNext())
                            return;
                        Assert.AreEqual("test_data_" + i + "_" + j, imageListEnumerator.Current.ImagePath);
                    }
                }
            for (var i = 1; i <= 5; i += 2)
                MemoryListDatabase.Delete(i);
            // 读，是否相等
            for (var i = 1; i <= 5; i++)
                using (var imageListEnumerator = MemoryImageDatabase.GetImageList(i).GetEnumerator())
                {
                    for (var j = 1; j <= 5; j++)
                    {
                        if (!imageListEnumerator.MoveNext())
                            return;
                        Assert.AreEqual("test_data_" + i + "_" + j, imageListEnumerator.Current.ImagePath);
                    }
                }
        }
    }
}