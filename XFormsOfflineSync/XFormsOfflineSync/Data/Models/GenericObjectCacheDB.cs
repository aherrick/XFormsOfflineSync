using SQLite;
using System;
using static XFormsOfflineSync.Data.Models.Enum;

namespace XFormsOfflineSync.Data.Models
{
    /// <summary>
    /// Generic Object Cache for Database.
    /// </summary>
    public class GenericObjectCacheDB
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public string JSONObject { get; set; }
        public DBType DBType { get; set; }
        public string ErrorMsg { get; set; }
    }
}