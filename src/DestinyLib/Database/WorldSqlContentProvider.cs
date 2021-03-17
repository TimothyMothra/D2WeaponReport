namespace DestinyLib.Database
{
    using System.Collections.Generic;

    using DestinyLib.DataContract;

    using Newtonsoft.Json;

    public class WorldSqlContentProvider
    {
        public readonly WorldSqlContent WorldSqlContent;

        public WorldSqlContentProvider(WorldSqlContent worldSqlContent) => this.WorldSqlContent = worldSqlContent;

        public WeaponDefinition GetWeaponDefinition(long id)
        {
            var record = this.WorldSqlContent.GetDestinyInventoryItemDefinition(id);
            
            // TODO: NULL CHECK
            dynamic jsonDynamic = JsonConvert.DeserializeObject(record);

            var weaponDefinition = new WeaponDefinition
            {
                MetaData = new WeaponDefinition.WeaponMetaData
                {
                    Id = jsonDynamic.hash,
                    Name = jsonDynamic.displayProperties.name,
                    AmmoTypeName = jsonDynamic.equippingBlock.ammoType.ToString(), //TODO: THIS
                    TierTypeName = jsonDynamic.inventory.tierTypeName,
                    DefaultDamageTypeHash = jsonDynamic.defaultDamageTypeHash,
                    FlavorText = jsonDynamic.flavorText,
                },
                Stats = new List<WeaponDefinition.WeaponStat>(),
                PerkSets = new List<WeaponDefinition.PerkSet>(),
            };

            return weaponDefinition;
        }

        /// <remarks>
        /// Source: (https://stackoverflow.com/questions/1202935/convert-rows-from-a-data-reader-into-typed-results).
        /// </remarks>
        /// <returns></returns>
        public IList<SearchableWeaponRecord> GetSearchableWeapons()
        {
            return this.WorldSqlContent.GetRecords(Properties.Resources.WorldSqlContent_GetAllWeapons, SearchableWeaponRecord.Parse);
        }
    }
}
