using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Data.Sqlite;
using Touch.Models;

namespace Touch.Data
{
    /// <summary>
    ///     存储文件夹路径的SQLite数据库
    /// </summary>
    public static class FolderDatabase
    {
        private const string TableName = "FolderTable";
        private const string PrimaryKeyName = "Primary_Key";
        private const string FolderPathName = "Folder_Path";
        private const string AccessTokenName = "Access_Token";

        /// <summary>
        ///     创建表
        /// </summary>
        public static void Create()
        {
            using (var db = new SqliteConnection("Filename=" + DatabaseHelper.DbFileName))
            {
                db.Open();
                const string createCommandStr = "CREATE TABLE IF NOT EXISTS " + TableName + " ("
                                                + PrimaryKeyName + " INTEGER PRIMARY KEY AUTOINCREMENT, "
                                                + FolderPathName + " NVARCHAR(2048) NULL, "
                                                + AccessTokenName + " NVARCHAR(2048) NULL)";
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
        /// <param name="folderPath">文件夹路径</param>
        /// <param name="accessToken">访问token</param>
        public static void Insert(string folderPath, string accessToken)
        {
            using (var db = new SqliteConnection("Filename=" + DatabaseHelper.DbFileName))
            {
                db.Open();
                var insertCommand = new SqliteCommand
                {
                    Connection = db,
                    CommandText = "INSERT INTO " + TableName + " VALUES (NULL, @" + FolderPathName + ", @" +
                                  AccessTokenName + ");"
                };
                // Use parameterized query to prevent SQL injection attacks
                insertCommand.Parameters.AddWithValue("@" + FolderPathName, folderPath);
                insertCommand.Parameters.AddWithValue("@" + AccessTokenName, accessToken);
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
        ///     依据文件夹路径删除一条记录
        /// </summary>
        /// <param name="folderPath">文件夹路径</param>
        public static void Delete(string folderPath)
        {
            using (var db = new SqliteConnection("Filename=" + DatabaseHelper.DbFileName))
            {
                db.Open();
                var deleteCommand = new SqliteCommand
                {
                    Connection = db,
                    CommandText = "DELETE FROM " + TableName + " WHERE " + FolderPathName + "=@" + FolderPathName + ";"
                };
                deleteCommand.Parameters.AddWithValue("@" + FolderPathName, folderPath);
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
        /// <returns>IEnumerable接口，所有的记录</returns>
        public static IEnumerable<MyFolder> GetFolders()
        {
            var folderList = new List<MyFolder>();
            using (var db = new SqliteConnection("Filename=" + DatabaseHelper.DbFileName))
            {
                db.Open();
                var selectCommand =
                    new SqliteCommand("SELECT " + FolderPathName + ", " + AccessTokenName + " from " + TableName, db);
                try
                {
                    var query = selectCommand.ExecuteReader();
                    while (query.Read())
                    {
                        var myFolder = new MyFolder
                        {
                            FolderPath = query.GetString(0),
                            AccessToken = query.GetString(1)
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

        /// <summary>
        ///     删除数据库表
        /// </summary>
        public static void Drop()
        {
            using (var db = new SqliteConnection("Filename=" + DatabaseHelper.DbFileName))
            {
                db.Open();
                const string dropCommandStr = "DROP TABLE IF EXISTS " + TableName;
                var dropCommand = new SqliteCommand(dropCommandStr, db);
                try
                {
                    dropCommand.ExecuteReader();
                }
                catch (SqliteException exception)
                {
                    Debug.WriteLine(exception);
                    throw;
                }
                db.Close();
            }
        }
    }
}