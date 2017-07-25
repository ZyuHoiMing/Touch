using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Touch.UnitTestProject.Models
{
    [TestClass]
    public class ImageListUnitTest
    {
        /// <summary>
        ///     在Assets目录下是否能获取到图片
        /// </summary>
        [TestMethod]
        public void GetInstanceTest()
        {
            //var taskSource = new TaskCompletionSource<object>();
            //await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
            //    CoreDispatcherPriority.Normal, async () =>
            //    {
            //        try
            //        {
            //            //var imageList = await ImageList.GetInstance(folder);
            //            //Assert.IsTrue(imageList.ImageModels.Count >= 7);
            //        }
            //        catch (Exception e)
            //        {
            //            taskSource.SetException(e);
            //        }
            //    });
            //await taskSource.Task;
        }
    }
}