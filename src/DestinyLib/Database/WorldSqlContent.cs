namespace DestinyLib.Database
{
    public class WorldSqlContent : Database
    {
        public WorldSqlContent(string connectionString) : base(connectionString) { }

        public string GetDestinyInventoryItemDefinition(uint id) => GetJsonRecord("DestinyInventoryItemDefinition", id);

        public string GetDestinyPlugSetDefinition(uint id) => GetJsonRecord("DestinyPlugSetDefinition", id);

        public string GetDestinyStatDefinition(uint id) => GetJsonRecord("DestinyStatDefinition", id);
    }
}
