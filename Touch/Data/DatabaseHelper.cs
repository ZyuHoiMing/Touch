using System;
using System.Diagnostics;
using Microsoft.Data.Sqlite.Internal;

namespace Touch.Data
{
    /// <summary>
    ///     数据库的通用配置
    /// </summary>
    public static class DatabaseHelper
    {
        /// <summary>
        ///     数据库文件名
        /// </summary>
        public const string DbFileName = "TouchSQLite.db";

        /// <summary>
        ///     初始化数据库，并为所有数据库创建表
        /// </summary>
        public static void InitDb()
        {
            // 初始化数据库
            try
            {
                // Configuring library to use SDK version of SQLite
                SqliteEngine.UseWinSqlite3();
            }
            catch (InvalidOperationException exception)
            {
                Debug.WriteLine(exception);
            }
            // 存储文件夹路径的
            FolderDatabase.Create();
            MemoryListDatabase.Create();
            MemoryImageDatabase.Create();
        }
    }
}