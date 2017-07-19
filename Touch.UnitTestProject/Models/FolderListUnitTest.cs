using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Touch.Data;
using Touch.Models;

namespace Touch.UnitTestProject.Models
{
    [TestClass]
    public class FolderListUnitTest
    {
        /// <summary>
        ///     测试初始化list后直接读数据，是否正确
        /// </summary>
        [TestMethod]
        public void ReadTest()
        {
            FolderDatabase.Init();
            FolderDatabase.Drop();
            FolderDatabase.Create();
            FolderDatabase.Insert("test_data_1");
            FolderDatabase.Insert("test_data_2");
            FolderDatabase.Insert("test_data_3");
            var folderList = new FolderList();
            var count = 1;
            foreach (var folder in folderList.List)
                Assert.AreEqual("test_data_" + count++, folder.FolderPath);
        }

        /// <summary>
        ///     添加新记录
        /// </summary>
        [TestMethod]
        public void AddTest()
        {
            FolderDatabase.Drop();
            FolderDatabase.Create();
            var folderList = new FolderList();
            folderList.Add(new MyFolder {FolderPath = "test_data_1"});
            folderList.Add(new MyFolder {FolderPath = "test_data_2"});
            folderList.Add(new MyFolder {FolderPath = "test_data_3"});
            // 在FolderList里读出来正常
            var count = 1;
            foreach (var folder in folderList.List)
                Assert.AreEqual("test_data_" + count++, folder.FolderPath);
            // 在数据库里读出正常
            var folders = FolderDatabase.GetFolders();
            count = 1;
            foreach (var folder in folders)
                Assert.AreEqual("test_data_" + count++, folder.FolderPath);
        }

        /// <summary>
        ///     删除记录测试
        /// </summary>
        [TestMethod]
        public void DeleteTest()
        {
            FolderDatabase.Drop();
            FolderDatabase.Create();
            var folderList = new FolderList();
            var testData = new List<MyFolder>
            {
                new MyFolder {FolderPath = "test_data_1"},
                new MyFolder {FolderPath = "test_data_2"},
                new MyFolder {FolderPath = "test_data_3"},
                new MyFolder {FolderPath = "test_data_4"},
                new MyFolder {FolderPath = "test_data_5"}
            };
            foreach (var data in testData)
                folderList.Add(data);
            folderList.Delete(testData[0]);
            folderList.Delete(testData[2]);
            folderList.Delete(testData[4]);
            // 在FolderList里读出来正常
            var count = 2;
            foreach (var folder in folderList.List)
            {
                Assert.AreEqual("test_data_" + count, folder.FolderPath);
                count += 2;
            }
            // 在数据库里读出正常
            var folders = FolderDatabase.GetFolders();
            count = 2;
            foreach (var folder in folders)
            {
                Assert.AreEqual("test_data_" + count, folder.FolderPath);
                count += 2;
            }
        }
    }
}