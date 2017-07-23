using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Data.Sqlite;
using Touch.Models;

namespace Touch.Data
{
    /// <summary>
    ///     回忆列表数据库
    /// </summary>
    public static class MemoryListDatabase
    {
        public const string TableName = "MemoryListTable";
        public const string PrimaryKeyName = "Primary_Key";
        private const string MemoryNameName = "Memory_Name";

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
                                                + MemoryNameName + " NVARCHAR(2048) NULL)";
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
        /// <param name="memoryName">回忆名字</param>
        public static void Insert(string memoryName)
        {
            using (var db = new SqliteConnection("Filename=" + DatabaseHelper.DbFileName))
            {
                db.Open();
                var insertCommand = new SqliteCommand
                {
                    Connection = db,
                    CommandText = "INSERT INTO " + TableName + " VALUES (NULL, @" + MemoryNameName + ");"
                };
                // Use parameterized query to prevent SQL injection attacks
                insertCommand.Parameters.AddWithValue("@" + MemoryNameName, memoryName);
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
        ///     依据主键号删除一条记录
        /// </summary>
        /// <param name="primaryKey">主键号</param>
        public static void Delete(int primaryKey)
        {
            using (var db = new SqliteConnection("Filename=" + DatabaseHelper.DbFileName))
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
        /// <returns>IEnumerable接口，所有的记录</returns>
        public static IEnumerable<MyMemory> GetMemoryList()
        {
            var memoryList = new List<MyMemory>();
            using (var db = new SqliteConnection("Filename=" + DatabaseHelper.DbFileName))
            {
                db.Open();
                var selectCommand =
                    new SqliteCommand("SELECT " + PrimaryKeyName + ", " + MemoryNameName + " from " + TableName, db);
                try
                {
                    var query = selectCommand.ExecuteReader();
                    while (query.Read())
                    {
                        var myMemory = new MyMemory
                        {
                            KeyNo = query.GetInt32(0),
                            MemoryName = query.GetString(1)
                        };
                        memoryList.Add(myMemory);
                    }
                }
                catch (SqliteException exception)
                {
                    Debug.WriteLine(exception);
                    throw;
                }
                db.Close();
            }
            return memoryList;
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