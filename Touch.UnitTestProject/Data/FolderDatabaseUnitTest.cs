using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Touch.Data;

namespace Touch.UnitTestProject.Data
{
    [TestClass]
    public class FolderDatabaseUnitTest
    {
        /// <summary>
        /// 如果已经存在数据库，删了重新创建一个
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CleanCreateTest()
        {
            var item = await ApplicationData.Current.LocalFolder.TryGetItemAsync(FolderDatabase.DbFileName);
            if (item != null)
            {
                await item.DeleteAsync();
            }
            FolderDatabase.Init();
            item = await ApplicationData.Current.LocalFolder.TryGetItemAsync(FolderDatabase.DbFileName);
            Assert.IsNotNull(item);
        }

        /// <summary>
        /// 重复初始化两次数据库
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DuplicateCreateTest()
        {
            FolderDatabase.Init();
            FolderDatabase.Init();
        }

        /// <summary>
        /// 插入并读出数据
        /// </summary>
        [TestMethod]
        public void InsertAndGetFoldersTest()
        {
            FolderDatabase.Insert("test_data_1");
            FolderDatabase.Insert("test_data_2");
            FolderDatabase.Insert("test_data_3");
            var folders = FolderDatabase.GetFolders();
            var count = 1;
            foreach (var folder in folders)
            {
                Assert.AreEqual("test_data_" + count++, folder.FolderPath);
            }
        }
        
        /// <summary>
        /// 删除并读出数据
        /// </summary>
        [TestMethod]
        public void DeleteAndGetFoldersTest()
        {
            // 之前插入了1 2 3
            FolderDatabase.Insert("test_data_4");
            FolderDatabase.Insert("test_data_5");
            FolderDatabase.Delete(1);
            FolderDatabase.Delete(3);
            FolderDatabase.Delete(5);
            var folders = FolderDatabase.GetFolders();
            var count = 2;
            foreach (var folder in folders)
            {
                Assert.AreEqual("test_data_" + count, folder.FolderPath);
                count += 2;
            }
        }
    }
}