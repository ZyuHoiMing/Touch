using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Touch.Data;
using Touch.ViewModels;

namespace Touch.UnitTestProject.ViewModels
{
    [TestClass]
    public class FolderListViewModelTest
    {
        /// <summary>
        ///     写读数据测试
        /// </summary>
        [UITestMethod]
        public void AddTest()
        {
            DatabaseHelper.InitDb();
            FolderDatabase.Drop();
            FolderDatabase.Create();
            var folderListVm = new FolderListViewModel();
            folderListVm.Add(new MyFolderViewModel {FolderPath = "test_data_1"});
            folderListVm.Add(new MyFolderViewModel {FolderPath = "test_data_2"});
            folderListVm.Add(new MyFolderViewModel {FolderPath = "test_data_3"});
            var count = folderListVm.MyFolderVms.Count;
            for (var i = 0; i < count - 1; i++)
                Assert.AreEqual("test_data_" + (i + 1), folderListVm.MyFolderVms[i].FolderPath);
        }

        /// <summary>
        ///     删除再读应该正常
        /// </summary>
        [UITestMethod]
        public void DeleteTest()
        {
            FolderDatabase.Drop();
            FolderDatabase.Create();
            var folderListVm = new FolderListViewModel();
            var testData = new List<MyFolderViewModel>
            {
                new MyFolderViewModel {FolderPath = "test_data_1"},
                new MyFolderViewModel {FolderPath = "test_data_2"},
                new MyFolderViewModel {FolderPath = "test_data_3"},
                new MyFolderViewModel {FolderPath = "test_data_4"},
                new MyFolderViewModel {FolderPath = "test_data_5"}
            };
            // 先加入5个数据
            foreach (var data in testData)
                folderListVm.Add(data);
            // 删除第0 2 4条记录
            for (var i = 4; i >= 0; i -= 2)
                folderListVm.Delete(testData[i]);
            // 读出来正常
            var count = folderListVm.MyFolderVms.Count;
            for (var i = 0; i < count - 1; i++)
                Assert.AreEqual("test_data_" + 2 * (i + 1), folderListVm.MyFolderVms[i].FolderPath);
        }
    }
}