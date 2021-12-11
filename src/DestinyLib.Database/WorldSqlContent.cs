namespace DestinyLib.Database
{
    using DestinyLib.Database.DataContract.Definitions;

    public class WorldSqlContent : BungieLib.Manifest.Database
    {
        public WorldSqlContent(string connectionString) : base(connectionString) { }

        /// <summary>
        /// This contains: Weapon definitions, Weapon Perk definitions, Intrinsic Trait definitions.
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

        /// <summary>
        /// This contains: Weapon Stat Interpolation Definitions.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDestinyStatGroupDefinition(uint id) => this.GetJsonRecord("DestinyStatGroupDefinition", id);

        public IList<SearchableWeaponRecord> GetSearchableWeapons()
        {
            // Source: (https://stackoverflow.com/questions/1202935/convert-rows-from-a-data-reader-into-typed-results).
            return this.GetRecords(Properties.Resources.WorldSqlContent_GetAllWeapons, SearchableWeaponRecord.Parse);
        }
    }
}
