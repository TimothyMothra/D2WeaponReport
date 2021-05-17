namespace DestinyLib.Database
{
    public class WorldSqlContent : Database
    {
        public WorldSqlContent(string connectionString) : base(connectionString) { }

        /// <summary>
        /// This contains: Weapon definitions.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDestinyInventoryItemDefinition(uint id) => GetJsonRecord("DestinyInventoryItemDefinition", id);

        /// <summary>
        /// This contains: Weapon Definition "Plugs", which are the unique collection of available perks.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDestinyPlugSetDefinition(uint id) => GetJsonRecord("DestinyPlugSetDefinition", id);

        /// <summary>
        /// This contains: Weapon Stats.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDestinyStatDefinition(uint id) => GetJsonRecord("DestinyStatDefinition", id);
    }
}
