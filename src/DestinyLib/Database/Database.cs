namespace DestinyLib.Database
{
    using System;
    using System.Collections.Generic;
    using System.Data;
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
        public static void TestConnection(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new IOException($"File does not exist: '{fileName}'");
            }

            var connectionString = MakeConnectionString(fileName);
            if (!TestConnectionInternal(connectionString))
            {
                throw new SystemException($"Test connection to database failed: {fileName}");
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

        public static string MakeConnectionString(string filePath) => $"Data Source={filePath}";
    }
}
