using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Data.Sqlite;
using Microsoft.Data.Sqlite.Internal;

namespace Touch.Data
{
    public class MyFolder
    {
        public string FolderPath { get; set; }
    }

    /// <summary>
    ///     存储文件夹路径的SQLite数据库
    /// </summary>
    public static class FolderDatabase
    {
        public const string DbFileName = "TouchSQLite.db";

        private const string TableName = "FolderTable";
        private const string PrimaryKeyName = "Primary_Key";
        private const string FolderPathName = "Folder_Path";

        /// <summary>
        ///     初始化数据库和表
        /// </summary>
        public static void Init()
        {
            try
            {
                // Configuring library to use SDK version of SQLite
                SqliteEngine.UseWinSqlite3();
            }
            catch (InvalidOperationException exception)
            {
                Debug.WriteLine(exception);
                throw;
            }
            using (var db = new SqliteConnection("Filename=" + DbFileName))
            {
                db.Open();
                const string createCommandStr = "CREATE TABLE IF NOT EXISTS " + TableName + " ("
                                                + PrimaryKeyName + " INTEGER PRIMARY KEY AUTOINCREMENT, "
                                                + FolderPathName + " NVARCHAR(2048) NULL)";
                var createCommand = new SqliteCommand(createCommandStr, db);
                try
                {
                    createCommand.ExecuteReader();
                }
                catch (SqliteException exception)
                {
                    Debug.WriteLine(exception);
                    throw;
                }
                db.Close();
            }
        }

        /// <summary>
        ///     添加一条记录
        /// </summary>
        /// <param name="folderPath"></param>
        public static void Insert(string folderPath)
        {
            using (var db = new SqliteConnection("Filename=" + DbFileName))
            {
                db.Open();
                var insertCommand = new SqliteCommand
                {
                    Connection = db,
                    CommandText = "INSERT INTO " + TableName + " VALUES (NULL, @" + FolderPathName + ");"
                };
                // Use parameterized query to prevent SQL injection attacks
                insertCommand.Parameters.AddWithValue("@" + FolderPathName, folderPath);
                try
                {
                    insertCommand.ExecuteReader();
                }
                catch (SqliteException exception)
                {
                    Debug.WriteLine(exception);
                    throw;
                }
                db.Close();
            }
        }

        /// <summary>
        ///     依据主键删除一条记录
        /// </summary>
        /// <param name="primaryKey"></param>
        public static void Delete(int primaryKey)
        {
            using (var db = new SqliteConnection("Filename=" + DbFileName))
            {
                db.Open();
                var deleteCommand = new SqliteCommand
                {
                    Connection = db,
                    CommandText = "DELETE FROM " + TableName + " WHERE " + PrimaryKeyName + "=@" + PrimaryKeyName + ";"
                };
                deleteCommand.Parameters.AddWithValue("@" + PrimaryKeyName, primaryKey);
                try
                {
                    deleteCommand.ExecuteReader();
                }
                catch (SqliteException exception)
                {
                    Debug.WriteLine(exception);
                    throw;
                }
                db.Close();
            }
        }

        /// <summary>
        ///     返回所有记录
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<MyFolder> GetFolders()
        {
            var folderList = new List<MyFolder>();
            using (var db = new SqliteConnection("Filename=" + DbFileName))
            {
                db.Open();
                var selectCommand =
                    new SqliteCommand("SELECT " + PrimaryKeyName + ", " + FolderPathName + " from " + TableName, db);
                try
                {
                    var query = selectCommand.ExecuteReader();
                    while (query.Read())
                    {
                        var myFolder = new MyFolder
                        {
                            FolderPath = query.GetString(1)
                        };
                        folderList.Add(myFolder);
                    }
                }
                catch (SqliteException exception)
                {
                    Debug.WriteLine(exception);
                    throw;
                }
                db.Close();
            }
            return folderList;
        }
    }
}