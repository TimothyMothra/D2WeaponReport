namespace DestinyLib.Database
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    using Microsoft.Data.Sqlite;

    public abstract class Database
    {
        public readonly string ConnectionString;

        public Database(string connectionString) => this.ConnectionString = connectionString;

        public static bool TestConnection(string fileName)
        {
            var connectionString = MakeConnectionString(fileName);

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
