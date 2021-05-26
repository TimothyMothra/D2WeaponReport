namespace DestinyLib.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using DestinyLib.DataContract;

    using Newtonsoft.Json;

    public class WorldSqlContentProvider
    {
        public readonly WorldSqlContent WorldSqlContent;

        private readonly ProviderOptions ProviderOptions;

        public WorldSqlContentProvider(WorldSqlContent worldSqlContent, ProviderOptions providerOptions)
        {
            this.WorldSqlContent = worldSqlContent; 
            this.ProviderOptions = providerOptions;
        }

        public WeaponDefinition GetWeaponDefinition(uint id)
        {
            var record = this.WorldSqlContent.GetDestinyInventoryItemDefinition(id);
            
            dynamic jsonDynamic = JsonConvert.DeserializeObject(record);
            if (jsonDynamic == null)
            {
                throw new Exception($"unexpected null result for {nameof(WorldSqlContent.GetDestinyInventoryItemDefinition)} id {id}");
            }

            var weaponDefinition = new WeaponDefinition
            {
                MetaData = new WeaponDefinition.WeaponMetaData
                {
                    Id = jsonDynamic.hash,
                    Name = jsonDynamic.displayProperties.name,
                    AmmoTypeId = jsonDynamic.equippingBlock.ammoType.ToString(), //TODO: Need to identify Ammo Type (example: ??)
                    TierTypeName = jsonDynamic.inventory.tierTypeName,
                    DefaultDamageTypeId = jsonDynamic.defaultDamageType,
                    DefaultDamageTypeHash = jsonDynamic.defaultDamageTypeHash,
                    FlavorText = jsonDynamic.flavorText,
                    ItemTypeId = jsonDynamic.itemSubType, //TODO: Need to identity item Sub Type (example: ??)
                },
                Stats = new List<WeaponDefinition.WeaponStat>(),
                PerkSets = new List<WeaponDefinition.PerkSet>(),
            };

            // Stats
            var statCollectionDynamic = jsonDynamic.stats.stats;
            foreach (var statDynamic in statCollectionDynamic)
            {
                uint statHash = statDynamic.Value.statHash;

                var statDefinition = this.GetWeaponStatDefinition(statHash);

                var stat = new WeaponDefinition.WeaponStat
                {
                    Name = statDefinition.Name,
                    Description = statDefinition.Description,
                    StatHash = statHash,
                    Value = statDynamic.Value.value,
                    MinValue = statDynamic.Value.minimum,
                    MaxValue = statDynamic.Value.maximum,
                    DisplayMaximum = statDynamic.Value.displayMaximum,
                };

                weaponDefinition.Stats.Add(stat);
            }

            // PERKS
            // Perks are stored in SocketEntries.
            // First, must read SocketCategory to identify which socket indexes hold WeaponPerks.
            // These are not cached because they are unique for each weapon definition.
            var socketCategoriesDynamic = jsonDynamic.sockets.socketCategories;
            var weaponPerkIndexes = Array.Empty<int>();
            foreach (var category in socketCategoriesDynamic)
            {
                if (category.socketCategoryHash == (uint)4241085061) // SocketCategory: "Weapon Perks"
                {
                    weaponPerkIndexes = category.socketIndexes.ToObject<int[]>();
                }
            }

            var socketEntriesDynamic = jsonDynamic.sockets.socketEntries;
            foreach(var i in weaponPerkIndexes)
            {
                var socketEntry = socketEntriesDynamic[i];
                // TODO: Bungie could introduce new things at anytime that break parsing.
                // Instead of referencing types to exclude, should identify types to include.
                if (socketEntry.socketTypeHash == (uint)1282012138 // ignore Tracker (example: ??)
                    || socketEntry.socketTypeHash == (uint)2575784089) // ignore Ticuu's Divination "stocks" (example: ??)
                {
                    continue;
                }

                var perkSet = new WeaponDefinition.PerkSet
                {
                    SocketIndex = i,
                    SocketTypeHash = socketEntry.socketTypeHash,
                    PlugSetHash = socketEntry.randomizedPlugSetHash ?? socketEntry.reusablePlugSetHash,
                    Perks = new List<WeaponDefinition.Perk>(),
                };

                // TODO: FETCHING A PERK SHOULD BE A SEPARATE METHOD. EASIER TO TEST
                var plugSetDefinitionRecord = this.WorldSqlContent.GetDestinyPlugSetDefinition(perkSet.PlugSetHash);
                dynamic plugSetDefinitionDynamic = JsonConvert.DeserializeObject(plugSetDefinitionRecord);
                foreach (var plug in plugSetDefinitionDynamic.reusablePlugItems)
                {
                    // TODO: Perk Definitions need to be cached
                    var perk = this.GetWeaponDefinitionPerk((uint)plug.plugItemHash);
                    perkSet.Perks.Add(perk);
                }

                weaponDefinition.PerkSets.Add(perkSet);
            }

            return weaponDefinition;
        }

        public WeaponDefinition.Perk GetWeaponDefinitionPerk(uint plugItemHash)
        {
            var perkRecord = this.WorldSqlContent.GetDestinyInventoryItemDefinition(plugItemHash);
            dynamic perkDynamic = JsonConvert.DeserializeObject(perkRecord);

            dynamic perkValuesDynamic = perkDynamic.investmentStats; // TODO: PARSE THIS COLLECTION

            var perkValues = new List<WeaponDefinition.PerkValue>();
            foreach (var perkValueDynamic in perkValuesDynamic)
            {
                var perkValue = new WeaponDefinition.PerkValue
                {
                    StatHash = perkValueDynamic.statTypeHash,
                    Value = perkValueDynamic.value,
                };

                perkValues.Add(perkValue);
            }

            return new WeaponDefinition.Perk
            {
                Id = plugItemHash,
                Name = perkDynamic.displayProperties.name,
                Description = perkDynamic.displayProperties.description,
                PerkValues = perkValues.Any() ? perkValues : null, // some perks may not have values that affect stats (example: Rampage). but others will (example: Field Prep).
            };
        }

        // TODO: These definitions need to be cached.
        public WeaponStatDefinition GetWeaponStatDefinition(uint statHash)
        {
            var record = this.WorldSqlContent.GetDestinyStatDefinition(statHash);

            dynamic jsonDynamic = JsonConvert.DeserializeObject(record);
            if (jsonDynamic == null)
            {
                throw new Exception($"unexpected null result for {nameof(WorldSqlContent.GetDestinyStatDefinition)} id {statHash}");
            }

            var weaponStatDefinition = new WeaponStatDefinition
            {
                Id = jsonDynamic.hash,
                Name = jsonDynamic.displayProperties.name,
                Description = jsonDynamic.displayProperties.description,
                Interpolate =jsonDynamic.interpolate
            };

            return weaponStatDefinition;
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
