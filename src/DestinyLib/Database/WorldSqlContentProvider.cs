namespace DestinyLib.Database
{
    using System;
    using System.Collections.Generic;

    using DestinyLib.DataContract;

    using Newtonsoft.Json;

    public class WorldSqlContentProvider
    {
        public readonly WorldSqlContent WorldSqlContent;

        public WorldSqlContentProvider(WorldSqlContent worldSqlContent) => this.WorldSqlContent = worldSqlContent;

        public WeaponDefinition GetWeaponDefinition(uint id)
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
                    AmmoTypeId = jsonDynamic.equippingBlock.ammoType.ToString(), //TODO: THIS
                    TierTypeName = jsonDynamic.inventory.tierTypeName,
                    DefaultDamageTypeId = jsonDynamic.defaultDamageType,
                    DefaultDamageTypeHash = jsonDynamic.defaultDamageTypeHash,
                    FlavorText = jsonDynamic.flavorText,
                    ItemTypeId = jsonDynamic.itemSubType, //TODO: THIS
                },
                Stats = new List<WeaponDefinition.WeaponStat>(),
                PerkSets = new List<WeaponDefinition.PerkSet>(),
            };

            // Stats
            var statCollectionDynamic = jsonDynamic.stats.stats;
            foreach (var statDynamic in statCollectionDynamic)
            {
                var stat = new WeaponDefinition.WeaponStat
                {
                    Name = "xxx", // TODO: This is in the DestinyStatDefinition table.
                    Description = "xxx", // TODO: This is in the DestinyStatDefinition table.
                    StatHash = statDynamic.Value.statHash,
                    Value = statDynamic.Value.value,
                    MinValue = statDynamic.Value.minimum,
                    MaxValue = statDynamic.Value.maximum,
                    DisplayMaximum = statDynamic.Value.displayMaximum,
                };
                weaponDefinition.Stats.Add(stat);
            }

            // Perks
            var socketCategoriesDynamic = jsonDynamic.sockets.socketCategories;
            var weaponPerkIndexes = Array.Empty<int>();
            foreach (var category in socketCategoriesDynamic)
            {
                if (category.socketCategoryHash == (uint)4241085061)
                {
                    weaponPerkIndexes = category.socketIndexes.ToObject<int[]>();
                }
            }

            var socketEntriesDynamic = jsonDynamic.sockets.socketEntries;
            foreach(var i in weaponPerkIndexes)
            {
                var socketEntry = socketEntriesDynamic[i];
                if (socketEntry.socketTypeHash == (uint)1282012138) // ignore Tracker
                {
                    continue;
                }

                var perkSet = new WeaponDefinition.PerkSet
                {
                    SocketIndex = i,
                    SocketTypeHash = socketEntry.socketTypeHash,
                    PlugSetHash = socketEntry.randomizedPlugSetHash,
                    //Perks = new List<WeaponDefinition.Perk>(),
                };

                var plugSetDefinitionRecord = this.WorldSqlContent.GetDestinyPlugSetDefinition(perkSet.PlugSetHash);
                dynamic plugSetDefinitionDynamic = JsonConvert.DeserializeObject(plugSetDefinitionRecord);
                foreach (var plug in plugSetDefinitionDynamic.reusablePlugItems)
                {
                    uint plugItemHash = plug.plugItemHash;

                    var perkRecord = this.WorldSqlContent.GetDestinyInventoryItemDefinition(plugItemHash);
                    dynamic perkDynamic = JsonConvert.DeserializeObject(perkRecord);
                    var perk = new WeaponDefinition.Perk
                    {
                        Id = plugItemHash,
                        Name = perkDynamic.displayProperties.name,
                        Description = perkDynamic.displayProperties.description
                    };
                    //perkSet.Perks.Add(perk);
                }

                weaponDefinition.PerkSets.Add(perkSet);
                // TODO: Perks are in the DestinyPlugSetDefinition table.


                

            }

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
