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
        private readonly string connectionString;

        public Database(string connectionString) => this.connectionString = connectionString;

        /// <summary>
        /// Test that a connection can be made to a provided database file.
        /// </summary>
        /// <remarks>
        /// As of today, the contents of the zip file match the file name of the downloaded zip. This could change in the future.
        /// </remarks>
        /// <param name="fileName"></param>
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

        public static string MakeConnectionString(FileInfo filePath) => $"Data Source={filePath.FullName}";

        public IList<T> GetRecords<T>(string commandText, Func<IDataRecord, T> buildObject)
        {
            var list = new List<T>();

            using (var connection = new SqliteConnection(this.connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = commandText;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(buildObject(reader));
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Get the Json record from the provided tableName and hashId.
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

            using (var connection = new SqliteConnection(this.connectionString))
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
    }
}
