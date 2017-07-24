using Microsoft.VisualStudio.TestTools.UnitTesting;
using Touch.Data;

namespace Touch.UnitTestProject.Data
{
    [TestClass]
    public class DatabaseHelperUnitTest
    {
        /// <summary>
        ///     重复初始化两次数据库
        /// </summary>
        [TestMethod]
        public void DuplicateCreateTest()
        {
            DatabaseHelper.InitDb();
            DatabaseHelper.InitDb();
        }
    }
}