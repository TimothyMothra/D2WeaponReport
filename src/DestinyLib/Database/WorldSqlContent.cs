namespace DestinyLib.Database
{
    public class WorldSqlContent : Database
    {
        public WorldSqlContent(string connectionString) : base(connectionString) { }

        /// <summary>
        /// This contains: Weapon definitions, Weapon Perk definitions.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDestinyInventoryItemDefinition(uint id) => this.GetJsonRecord("DestinyInventoryItemDefinition", id);

        /// <summary>
        /// This contains: weapon icons.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDestinyCollectibleDefinition(uint id) => this.GetJsonRecord("DestinyCollectibleDefinition", id);

        /// <summary>
        /// This contains: Weapon Definition "Plugs", which are the unique collection of available perks.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDestinyPlugSetDefinition(uint id) => this.GetJsonRecord("DestinyPlugSetDefinition", id);

        /// <summary>
        /// This contains: Weapon Stats.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDestinyStatDefinition(uint id) => this.GetJsonRecord("DestinyStatDefinition", id);
    }
}
