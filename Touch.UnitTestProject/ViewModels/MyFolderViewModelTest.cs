using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Touch.ViewModels;

namespace Touch.UnitTestProject.ViewModels
{
    [TestClass]
    public class MyFolderViewModelTest
    {
        /// <summary>
        ///     set get测试
        /// </summary>
        [UITestMethod]
        public void SetAndGetTest()
        {
            var myFolderVm = new MyFolderViewModel {FolderPath = "test_1"};
            Assert.AreEqual("test_1", myFolderVm.FolderPath);
            myFolderVm.FolderPath = "test_2";
            Assert.AreEqual("test_2", myFolderVm.FolderPath);
        }

        /// <summary>
        ///     存的内容改变了，是否会通知事件
        /// </summary>
        [UITestMethod]
        public void PropertyChangedTest()
        {
            var myFolderVm = new MyFolderViewModel();
            var count = 0;
            myFolderVm.PropertyChanged += (sender, args) => { count++; };
            Assert.AreEqual(0, count);
            myFolderVm.FolderPath = "test_1";
            Assert.AreEqual(1, count);
            myFolderVm.FolderPath = "test_2";
            Assert.AreEqual(2, count);
        }
    }
}