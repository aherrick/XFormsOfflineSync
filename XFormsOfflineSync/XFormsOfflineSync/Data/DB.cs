using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using XFormsOfflineSync.Data.Models;
using static XFormsOfflineSync.Data.Models.Enum;

namespace XFormsOfflineSync.Data
{
    public class DB
    {
        private string dbPath = null;
        private static object collisionLock = new object();

        public DB(string dbPath)
        {
            this.dbPath = dbPath;

            CreateTable<GenericObjectCacheDB>();
        }

        /// <summary>
        /// Save Generic Object DB.
        /// </summary>
        /// <param name="json">JSON Object String.</param>
        /// <param name="type">Object Type.</param>
        /// <returns>Generic Object DB.</returns>
        private GenericObjectCacheDB SaveGenericObjectDB(string json, DBType type)
        {
            var obj = new GenericObjectCacheDB
            {
                JSONObject = json,
                DBType = type,
                CreatedOn = DateTime.UtcNow
            };

            Insert(obj);

            return obj;
        }

        /// Get Generic Object List.
        /// </summary>
        /// <returns>Generic Object List.</returns>
        public List<GenericObjectCacheDB> GetGenericObjectDBs()
        {
            lock (collisionLock)
            {
                using (var database = new SQLiteConnection(dbPath))
                {
                    return database.Table<GenericObjectCacheDB>().OrderBy(x => x.Id).ToList();
                }
            }
        }

        #region Generic Table Methods

        /// <summary>
        /// Drop and Create Generic Table.
        /// </summary>
        /// <typeparam name="T">Table Class.</typeparam>
        public void ResetTable<T>()
        {
            DropTable<T>();
            CreateTable<T>();
        }

        /// <summary>
        /// Drop Generic Table.
        /// </summary>
        /// <typeparam name="T">Table Class.</typeparam>
        private void DropTable<T>()
        {
            lock (collisionLock)
            {
                using (var database = new SQLiteConnection(dbPath))
                {
                    database.DropTable<T>();
                }
            }
        }

        /// <summary>
        /// Create Generic Table.
        /// </summary>
        /// <typeparam name="T">Table Class.</typeparam>
        private void CreateTable<T>()
        {
            lock (collisionLock)
            {
                using (var database = new SQLiteConnection(dbPath))
                {
                    database.CreateTable<T>();
                }
            }
        }

        /// <summary>
        /// Check if Generic Table Exists.
        /// </summary>
        /// <typeparam name="T">Table Class Type.</typeparam>
        /// <returns>If Table Exists.</returns>
        public bool TableExists<T>()
        {
            lock (collisionLock)
            {
                using (var database = new SQLiteConnection(dbPath))
                {
                    const string cmdText = "SELECT name FROM sqlite_master WHERE type='table' AND name=?";
                    var cmd = database.CreateCommand(cmdText, typeof(T).Name);
                    return cmd.ExecuteScalar<string>() != null;
                }
            }
        }

        /// <summary>
        /// Insert Generic Table.
        /// </summary>
        /// <typeparam name="T">Table Class Type.</typeparam>
        /// <param name="item">Table Class Object.</param>
        public void Insert<T>(T item)
        {
            lock (collisionLock)
            {
                using (var database = new SQLiteConnection(dbPath))
                {
                    database.Insert(item, typeof(T));
                }
            }
        }

        /// <summary>
        /// Insert Or Replace Generic Table.
        /// </summary>
        /// <typeparam name="T">Table Class Type.</typeparam>
        /// <param name="item">Table Class Object.</param>
        public void InsertOrReplaceMultiple<T>(List<T> items)
        {
            foreach (var item in items)
            {
                InsertOrReplace(item);
            }
        }

        /// <summary>
        /// Insert Or Replace Generic Table.
        /// </summary>
        /// <typeparam name="T">Table Class Type.</typeparam>
        /// <param name="item">Table Class Object.</param>
        public void InsertOrReplace<T>(T item)
        {
            lock (collisionLock)
            {
                using (var database = new SQLiteConnection(dbPath))
                {
                    database.InsertOrReplace(item, typeof(T));
                }
            }
        }

        /// <summary>
        /// Get Genric Table List.
        /// </summary>
        /// <typeparam name="T">Table Class Type.</typeparam>
        /// <returns>Generic Table List.</returns>
        public List<T> GetData<T>() where T : new()
        {
            lock (collisionLock)
            {
                using (var database = new SQLiteConnection(dbPath))
                {
                    return database.Table<T>().ToList();
                }
            }
        }

        /// <summary>
        /// Gets the data count.
        /// </summary>
        /// <typeparam name="T">Table Class Type.</typeparam>
        /// <returns>Count.</returns>
        public int GetDataCount<T>() where T : new()
        {
            lock (collisionLock)
            {
                using (var database = new SQLiteConnection(dbPath))
                {
                    return database.Table<T>().Count();
                }
            }
        }

        /// <summary>
        /// Delte Generic Table.
        /// </summary>
        /// <typeparam name="T">Table Class Type.</typeparam>
        /// <param name="item">Table Class Object.</param>
        public void Delete<T>(T item)
        {
            lock (collisionLock)
            {
                using (var database = new SQLiteConnection(dbPath))
                {
                    database.Delete(item);
                }
            }
        }

        /// <summary>
        /// Delete All Generic Table Objects.
        /// </summary>
        /// <typeparam name="T">Table Class Type.</typeparam>
        public void DeleteAll<T>()
        {
            lock (collisionLock)
            {
                using (var database = new SQLiteConnection(dbPath))
                {
                    var query = string.Format("delete from \"{0}\"", nameof(T));
                    database.Execute(query);
                }
            }
        }

        #endregion Generic Table Methods
    }
}