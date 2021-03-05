namespace DestinyLib.DataContract
{
    using System.Collections.Generic;

    using DestinyLib.Database;

    using Newtonsoft.Json;

    public class DataController
    {
        public readonly WorldSqlContent WorldSqlContent;

        public DataController(WorldSqlContent worldSqlContent)
        {
            this.WorldSqlContent = worldSqlContent;
        }

        public WeaponDefinition GetWeaponDefinition(int id)
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

    }
}
