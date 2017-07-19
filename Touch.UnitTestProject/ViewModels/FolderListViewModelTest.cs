using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        [TestMethod]
        public void AddTest()
        {
            FolderDatabase.Init();
            FolderDatabase.Drop();
            FolderDatabase.Create();
            var folderListVm = new FolderListViewModel();
            folderListVm.Add(new MyFolderViewModel {FolderPath = "test_data_1"});
            folderListVm.Add(new MyFolderViewModel {FolderPath = "test_data_2"});
            folderListVm.Add(new MyFolderViewModel {FolderPath = "test_data_3"});
            var count = 1;
            foreach (var myFolderVm in folderListVm.MyFolderVms)
                Assert.AreEqual("test_data_" + count++, myFolderVm.FolderPath);
        }

        /// <summary>
        ///     删除再读应该正常
        /// </summary>
        [TestMethod]
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
            {
                folderListVm.SelectedIndex = i;
                folderListVm.Delete();
            }
            // 读出来正常
            var count = 2;
            foreach (var myFolderVm in folderListVm.MyFolderVms)
            {
                Assert.AreEqual("test_data_" + count, myFolderVm.FolderPath);
                count += 2;
            }
        }
    }
}