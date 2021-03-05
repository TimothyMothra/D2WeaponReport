namespace DestinyLib.Database
{
    using System;

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
    }
}
