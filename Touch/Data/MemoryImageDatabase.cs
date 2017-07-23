using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Data.Sqlite;
using Touch.Models;

namespace Touch.Data
{
    /// <summary>
    ///     回忆里的图片路径的数据库
    /// </summary>
    public static class MemoryImageDatabase
    {
        private const string TableName = "MemoryImageTable";
        private const string PrimaryKeyName = "Primary_Key";
        private const string ForeignKeyName = "Foreign_Key";
        private const string ImagePathName = "Image_Path";
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
                                                + ForeignKeyName + " INTEGER, "
                                                + ImagePathName + " NVARCHAR(2048) NULL, "
                                                + AccessTokenName + " NVARCHAR(2048) NULL, "
                                                + "FOREIGN KEY(" + ForeignKeyName + ") REFERENCES " +
                                                MemoryListDatabase.TableName + "(" + MemoryListDatabase.PrimaryKeyName +
                                                "))";
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
        /// <param name="keyNo"></param>
        /// <param name="imagePath"></param>
        /// <param name="accessToken"></param>
        public static void Insert(int keyNo, string imagePath, string accessToken)
        {
            using (var db = new SqliteConnection("Filename=" + DatabaseHelper.DbFileName))
            {
                db.Open();
                var insertCommand = new SqliteCommand
                {
                    Connection = db,
                    CommandText = "INSERT INTO " + TableName + " VALUES (NULL, @" + ForeignKeyName + ", @" +
                                  ImagePathName + ", @" + AccessTokenName + ");"
                };
                // Use parameterized query to prevent SQL injection attacks
                insertCommand.Parameters.AddWithValue("@" + ForeignKeyName, keyNo);
                insertCommand.Parameters.AddWithValue("@" + ImagePathName, imagePath);
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
        ///     依据外键号删除一系列记录
        /// </summary>
        /// <param name="foreignKey">外键号</param>
        public static void Delete(int foreignKey)
        {
            using (var db = new SqliteConnection("Filename=" + DatabaseHelper.DbFileName))
            {
                db.Open();
                var deleteCommand = new SqliteCommand
                {
                    Connection = db,
                    CommandText = "DELETE FROM " + TableName + " WHERE " + ForeignKeyName + "=@" + ForeignKeyName + ";"
                };
                deleteCommand.Parameters.AddWithValue("@" + ForeignKeyName, foreignKey);
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
        ///     返回某个外键的所有记录
        /// </summary>
        /// <param name="foreignKey">外键号</param>
        /// <returns>IEnumerable接口，所有的记录</returns>
        public static IEnumerable<MyImage> GetImageList(int foreignKey)
        {
            var imageList = new List<MyImage>();
            using (var db = new SqliteConnection("Filename=" + DatabaseHelper.DbFileName))
            {
                db.Open();
                var selectCommand =
                    new SqliteCommand(
                        "SELECT " + ImagePathName + ", " + AccessTokenName + " FROM " + TableName + " WHERE " +
                        ForeignKeyName + "=@" + ForeignKeyName, db);
                selectCommand.Parameters.AddWithValue("@" + ForeignKeyName, foreignKey);
                try
                {
                    var query = selectCommand.ExecuteReader();
                    while (query.Read())
                    {
                        var myImage = new MyImage
                        {
                            ImagePath = query.GetString(0),
                            AccessToken = query.GetString(1)
                        };
                        imageList.Add(myImage);
                    }
                }
                catch (SqliteException exception)
                {
                    Debug.WriteLine(exception);
                    throw;
                }
                db.Close();
            }
            return imageList;
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