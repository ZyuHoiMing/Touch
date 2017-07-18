using System;
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
        /// <returns></returns>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DuplicateCreateTest()
        {
            FolderDatabase.Init();
            FolderDatabase.Init();
        }
    }
}