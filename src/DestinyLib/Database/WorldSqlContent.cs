namespace DestinyLib.Database
{
    using System.Text.Json;

    using DestinyLib.DataContract;

    using Microsoft.Data.Sqlite;

    public class WorldSqlContent
    {
        private string connectionString;

        public WorldSqlContent(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DestinyInventoryItemDefinition_Weapon GetSingleWeapon(int id) => GetRecord<DestinyInventoryItemDefinition_Weapon>("DestinyInventoryItemDefinition", id);

        public T GetRecord<T>(string tableName, int id)
        {
            var record = GetRecord(tableName: tableName, id: id);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Deserialize<T>(record, options);
        }

        public string GetRecord(string tableName, int id)
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

            return default;
        }
    }
}
