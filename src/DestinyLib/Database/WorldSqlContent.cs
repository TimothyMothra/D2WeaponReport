namespace DestinyLib.Database
{
    using System;

    using DataContract;

    using Microsoft.Data.Sqlite;

    using Newtonsoft.Json;

    public class WorldSqlContent
    {
        private string connectionString;

        public WorldSqlContent(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //public WeaponItemDefinition GetWeaponItemDefinition(int id) => WeaponItemDefinition.Parse(GetJsonRecord("DestinyInventoryItemDefinition", id));

        public dynamic GetDestinyInventoryItemDefinition(int id) => GetJsonDynamic("DestinyInventoryItemDefinition", id);

        public dynamic GetJsonDynamic(string tableName, int id)
        {
            //TODO: NULL CHECK
            var jsonRecord = GetJsonRecord(tableName, id);
            return JsonConvert.DeserializeObject(jsonRecord);
        }

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
