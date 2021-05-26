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

        private readonly Dictionary<uint, WeaponStatDefinition> WeaponStatDefinitionCache = new Dictionary<uint, WeaponStatDefinition>();
        private readonly Dictionary<uint, WeaponDefinition.Perk> WeaponDefinitionPerkCache = new Dictionary<uint, WeaponDefinition.Perk>();

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
            #region Weapon Definition Stats
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
            #endregion

            
            // PERKS
            #region WeaponDefinition Perks
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
                var socketEntryDynamic = socketEntriesDynamic[i];
                // TODO: Instead of referencing types to exclude, should identify types to include.
                // Bungie could introduce new things at anytime that break parsing.
                if (socketEntryDynamic.socketTypeHash == (uint)1282012138 // ignore Tracker (example: ??)
                    || socketEntryDynamic.socketTypeHash == (uint)2575784089) // ignore Ticuu's Divination "stocks" (example: ??)
                {
                    continue;
                }

                //TODO: Are PerkSets ever reused or unique to weapon? I don't think this needs to be cached but might be wrong.
                var perkSet = new WeaponDefinition.PerkSet
                {
                    SocketIndex = i,
                    SocketTypeHash = socketEntryDynamic.socketTypeHash, //TODO: RELATED TO ABOVE COMMENT. I DON'T KNOW WHAT ALL OF THESE ARE YET.
                    PlugSetHash = socketEntryDynamic.randomizedPlugSetHash ?? socketEntryDynamic.reusablePlugSetHash,
                    Perks = null,
                };
                perkSet.Perks = this.GetWeaponDefinitionPerks(perkSet.PlugSetHash);

                weaponDefinition.PerkSets.Add(perkSet);
            }
#endregion

            return weaponDefinition;
        }

        internal List<WeaponDefinition.Perk> GetWeaponDefinitionPerks(uint plugSetHash)
        {
            var perks = new List<WeaponDefinition.Perk>();

            var plugSetDefinitionRecord = this.WorldSqlContent.GetDestinyPlugSetDefinition(plugSetHash);
            dynamic plugSetDefinitionDynamic = JsonConvert.DeserializeObject(plugSetDefinitionRecord);
            foreach (var plug in plugSetDefinitionDynamic.reusablePlugItems)
            {
                // TODO: Perk Definitions need to be cached
                var perk = this.GetWeaponDefinitionPerk((uint)plug.plugItemHash);
                perks.Add(perk);
            }

            return perks;
        }

        internal WeaponDefinition.Perk GetWeaponDefinitionPerk(uint plugItemHash)
        {
            if (this.ProviderOptions.EnableCaching && this.WeaponDefinitionPerkCache.TryGetValue(plugItemHash, out var cachedRecord))
            {
                return cachedRecord;
            }


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

            var perk = new WeaponDefinition.Perk
            {
                Id = plugItemHash,
                Name = perkDynamic.displayProperties.name,
                Description = perkDynamic.displayProperties.description,
                PerkValues = perkValues.Any() ? perkValues : null, // some perks may not have values that affect stats (example: Rampage). but others will (example: Field Prep).
            };


            if (this.ProviderOptions.EnableCaching)
            {
                this.WeaponDefinitionPerkCache.Add(plugItemHash, perk);
            }

            return perk;
        }

        // TODO: These definitions need to be cached.
        public WeaponStatDefinition GetWeaponStatDefinition(uint statHash)
        {
            if (this.ProviderOptions.EnableCaching && this.WeaponStatDefinitionCache.TryGetValue(statHash, out var cachedRecord))
            {
                return cachedRecord;
            }

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

            if (this.ProviderOptions.EnableCaching)
            {
                this.WeaponStatDefinitionCache.Add(statHash, weaponStatDefinition);
            }

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
