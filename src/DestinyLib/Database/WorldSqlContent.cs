namespace DestinyLib.Database
{
    using DataContract;

    using Microsoft.Data.Sqlite;

    public class WorldSqlContent
    {
        private string connectionString;

        public WorldSqlContent(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public WeaponItemDefinition GetSingleWeapon(int id) => WeaponItemDefinition.Parse(GetJsonRecord("DestinyInventoryItemDefinition", id));

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

            return default;
        }
    }
}
