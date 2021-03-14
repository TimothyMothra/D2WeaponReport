namespace DestinyLib.Database
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    using Microsoft.Data.Sqlite;

    public class WorldSqlContent
    {
        private readonly string connectionString;

        public WorldSqlContent(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string GetDestinyInventoryItemDefinition(int id) => GetJsonRecord("DestinyInventoryItemDefinition", id);

        public string GetJsonRecord(string tableName, int id)
        {
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

            throw new Exception("Sqlite query returned no results.");
        }

        public IList<T> GetRecords<T>(string commandText, Func<IDataRecord, T> BuildObject)
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
                        list.Add(BuildObject(reader));
                    }
                }
            }

            return list;
        }
    }
}
