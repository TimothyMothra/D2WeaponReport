namespace DestinyLib.Database
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.IO;

    using Microsoft.Data.Sqlite;

    public abstract class Database
    {
        public readonly string ConnectionString;

        public Database(string connectionString) => this.ConnectionString = connectionString;

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// As of today, the contents of the zip file match the file name of the downloaded zip. This could change in the future.
        /// </remarks>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static void TestConnection(FileInfo fileName)
        {
            if (!fileName.Exists)
            {
                throw new IOException($"File does not exist: '{fileName.FullName}'");
            }

            var connectionString = MakeConnectionString(fileName);
            if (!TestConnectionInternal(connectionString))
            {
                throw new SystemException($"Test connection to database failed: {fileName.FullName}");
            }
        }

        private static bool TestConnectionInternal(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT date('now')";
                var result = command.ExecuteScalar();

                return DateTime.TryParse(result.ToString(), out _);
            }
        }

        public IList<T> GetRecords<T>(string commandText, Func<IDataRecord, T> BuildObject)
        {
            var list = new List<T>();

            using (var connection = new SqliteConnection(this.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = commandText;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(BuildObject(reader));
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// In the API, hashes (like item hashes) are can be represented as an "unsigned 32 int". 
        /// However, in the SQLiteDB, these hashes are represented as a "signed 32 int". 
        /// The unsigned 32 int overflows what can be stored in SQLite's signed 32 int, 
        /// so the majority of items in the DB will have negative ids. 
        /// For example, the item "Vigil of Heroes" with item hash 2592351697 has an ID value of -1702615599 in the definitions SQLite database.
        /// To query the SQLite DB for a hash, you will need to convert the hash from an "unsigned 32 int" to a "signed 32 int".
        /// (https://github.com/vpzed/Destiny2-API-Info/wiki/API-Introduction-Part-3-Manifest#converting-hashes-for-the-sqlite-db).
        /// </remarks>
        public string GetJsonRecord(string tableName, uint hashId)
        {
            var id = unchecked((int)hashId);

            using (var connection = new SqliteConnection(this.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT json FROM {tableName} WHERE id = $id";
                command.Parameters.AddWithValue("$id", id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }

            Debug.Fail("Sqlite query returned no results.");

            return null;
        }

        public static string MakeConnectionString(FileInfo filePath) => $"Data Source={filePath.FullName}";
    }
}
