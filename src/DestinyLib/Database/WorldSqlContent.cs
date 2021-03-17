namespace DestinyLib.Database
{
    using System;

    using Microsoft.Data.Sqlite;

    public class WorldSqlContent : Database
    {
        public WorldSqlContent(string connectionString) : base(connectionString) { }

        public string GetDestinyInventoryItemDefinition(long id) => GetJsonRecord("DestinyInventoryItemDefinition", id);

        public string GetJsonRecord(string tableName, long id)
        {
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

            throw new Exception("Sqlite query returned no results.");
        }
    }
}
