namespace DestinyLib.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using DestinyLib.Database.DataContract.Definitions;

    using Newtonsoft.Json;

    using static DestinyLib.Database.DataContract.Definitions.WeaponStatGroupDefinition;

    public class WorldSqlContentProvider
    {
        private readonly WorldSqlContent worldSqlContent;

        private readonly ProviderOptions providerOptions;

        private readonly Dictionary<uint, WeaponStatMetaData> weaponStatDefinitionCache = new ();
        private readonly Dictionary<uint, WeaponPerkDefinition> weaponDefinitionPerkCache = new ();
        private readonly Dictionary<uint, DestinyCollectibleDefinition> destinyCollectibleDefinitionCache = new ();

        public WorldSqlContentProvider(WorldSqlContent worldSqlContent, ProviderOptions providerOptions)
        {
            this.worldSqlContent = worldSqlContent;
            this.providerOptions = providerOptions;
        }

        public WeaponDefinition GetWeaponDefinition(uint id)
        {
            var record = this.worldSqlContent.GetDestinyInventoryItemDefinition(id);
            if (record == null)
            {
                throw new Exception($"unexpected null result for {nameof(this.worldSqlContent.GetDestinyInventoryItemDefinition)} id {id}");
            }

            dynamic jsonDynamic = JsonConvert.DeserializeObject(record);
            if (jsonDynamic == null)
            {
                throw new Exception($"unexpected null result for {nameof(this.worldSqlContent.GetDestinyInventoryItemDefinition)} id {id}");
            }

            var weaponMetaData = new WeaponMetaData
            {
                HashId = jsonDynamic.hash,
                Name = jsonDynamic.displayProperties.name,
                TypeName = jsonDynamic.itemTypeDisplayName,
                ItemDefinitionIconPath = jsonDynamic.displayProperties.icon,
                ScreenshotPath = jsonDynamic.screenshot,
                AmmoTypeId = jsonDynamic.equippingBlock.ammoType.ToString(), //TODO: Need to identify Ammo Type (example: "Energy Weapons")
                TierTypeName = jsonDynamic.inventory.tierTypeName,
                DefaultDamageTypeId = jsonDynamic.defaultDamageType,
                DefaultDamageTypeHash = jsonDynamic.defaultDamageTypeHash,
                CollectibleHash = jsonDynamic.collectibleHash ?? default(uint),
                FlavorText = jsonDynamic.flavorText,
                ItemTypeId = jsonDynamic.itemSubType, //TODO: Need to identity Weapon Type (example: "enum DestinyItemSubType "AutoRifle"")
            };

            // check Collection for Seasonal Weapon Icon (Note: does not exist for all weapons).
            if (weaponMetaData.CollectibleHash != default)
            {
                var collectibleDefinition = this.GetDestinyCollectibleDefinitions(weaponMetaData.CollectibleHash);
                weaponMetaData.CollectionDefintitionIconPath = collectibleDefinition.IconPath;
            }

            // Stats
            #region Weapon Definition Stats
            var weaponStatsCollection = new WeaponStatsCollection();

            var statCollectionDynamic = jsonDynamic.stats.stats;
            foreach (var statDynamic in statCollectionDynamic)
            {
                uint statHash = statDynamic.Value.statHash;

                var statDefinition = this.GetWeaponStatDefinition(statHash);

                var stat = new WeaponStatDefinition(statDefinition)
                {
                    Value = statDynamic.Value.value,
                    MinValue = statDynamic.Value.minimum,
                    MaxValue = statDynamic.Value.maximum,
                    DisplayMaximum = statDynamic.Value.displayMaximum,
                };

                // ASSUMPTION: MaxValue is never used.
                if (stat.MaxValue != 0)
                {
                    throw new ($"weapon id {id} name {weaponMetaData.Name} | stat id {stat.MetaData.HashId} name {stat.MetaData.Name} value {stat.Value} max {stat.MaxValue} displayMax {stat.DisplayMaximum}");
                }

                weaponStatsCollection.Values.Add(stat);
            }

            uint statGroupHashId = jsonDynamic.stats.statGroupHash;
            weaponStatsCollection.StatGroupDefinition = this.GetWeaponStatGroupDefinition(statGroupHashId);
            #endregion

            // PERKS
            #region WeaponDefinition Perks

            var weaponPerksCollection = new WeaponPerkSetCollection();

            // Perks are stored in SocketEntries.
            // First, must read SocketCategory to identify which socket indexes hold WeaponPerks.
            // These are not cached because they are unique for each weapon definition.
            var socketCategoriesDynamic = jsonDynamic.sockets.socketCategories;
            var weaponPerkIndexes = Array.Empty<int>();
            var intrinsicTraitsIndex = -1;
            foreach (var category in socketCategoriesDynamic)
            {
                if (category.socketCategoryHash == (uint)3956125808) // SocketCategory: "Intrinsic Traits"
                {
                    var indexes = category.socketIndexes.ToObject<int[]>();

                    // ASSUMPTION: There is only 1 intrinsic trait
                    if (indexes.Length != 1)
                    {
                        throw new ($"weapon id {id} name {weaponMetaData.Name} | intrinsic traits: {indexes.Length}");
                    }

                    intrinsicTraitsIndex = indexes[0];
                }

                if (category.socketCategoryHash == (uint)4241085061) // SocketCategory: "Weapon Perks"
                {
                    weaponPerkIndexes = category.socketIndexes.ToObject<int[]>();
                }
            }

            var socketEntriesDynamic = jsonDynamic.sockets.socketEntries;

            // Get FrameName from Intrinsic Traits.
            if (intrinsicTraitsIndex != -1)
            {
                var intrinsicTraitDynamic = socketEntriesDynamic[intrinsicTraitsIndex];

                uint frameHashId = intrinsicTraitDynamic.singleInitialItemHash;

                var intrinsicRecord = this.worldSqlContent.GetDestinyInventoryItemDefinition(frameHashId);
                dynamic intrinsicDynamic = JsonConvert.DeserializeObject(intrinsicRecord);

                weaponMetaData.FrameName = intrinsicDynamic.displayProperties.name;
                weaponMetaData.FrameDescription = intrinsicDynamic.displayProperties.description;
            }

            // Get Perks
            foreach (var i in weaponPerkIndexes)
            {
                var socketEntryDynamic = socketEntriesDynamic[i];
                // TODO: Instead of referencing types to exclude, should identify types to include.
                // Bungie could introduce new things at anytime that break parsing.
                if (socketEntryDynamic.socketTypeHash == 1282012138u // ignore Tracker (example: ??)
                     || socketEntryDynamic.socketTypeHash == 2575784089u) // ignore Ticuu's Divination "stocks" (example: ??)
                {
                    continue;
                }

                //TODO: Are PerkSets ever reused or unique to weapon? I don't think this needs to be cached but might be wrong.
                var perkSet = new WeaponPerkSetDefinition
                {
                    SocketIndex = i,
                    SocketTypeHash = socketEntryDynamic.socketTypeHash, //TODO: RELATED TO ABOVE COMMENT. I DON'T KNOW WHAT ALL OF THESE ARE YET.
                    PlugSetHashId = socketEntryDynamic.randomizedPlugSetHash ?? socketEntryDynamic.reusablePlugSetHash,
                    Values = null,
                };
                perkSet.Values = this.GetWeaponDefinitionPerks(perkSet.PlugSetHashId);

                weaponPerksCollection.Values.Add(perkSet);
            }
            #endregion

            return new WeaponDefinition(weaponMetaData, weaponStatsCollection, weaponPerksCollection);
        }

        public WeaponStatMetaData GetWeaponStatDefinition(uint statHash)
        {
            if (this.providerOptions.EnableCaching && this.weaponStatDefinitionCache.TryGetValue(statHash, out var cachedRecord))
            {
                return cachedRecord;
            }

            var record = this.worldSqlContent.GetDestinyStatDefinition(statHash);

            dynamic jsonDynamic = JsonConvert.DeserializeObject(record);
            if (jsonDynamic == null)
            {
                throw new Exception($"unexpected null result for {nameof(this.worldSqlContent.GetDestinyStatDefinition)} id {statHash}");
            }

            var weaponStatDefinition = new WeaponStatMetaData
            {
                HashId = jsonDynamic.hash,
                Name = jsonDynamic.displayProperties.name,
                Description = jsonDynamic.displayProperties.description,
                Interpolate = jsonDynamic.interpolate,
            };

            if (this.providerOptions.EnableCaching)
            {
                this.weaponStatDefinitionCache.Add(statHash, weaponStatDefinition);
            }

            return weaponStatDefinition;
        }

        public DestinyCollectibleDefinition GetDestinyCollectibleDefinitions(uint collectibleHash)
        {
            if (this.providerOptions.EnableCaching && this.destinyCollectibleDefinitionCache.TryGetValue(collectibleHash, out var cachedRecord))
            {
                return cachedRecord;
            }

            var record = this.worldSqlContent.GetDestinyCollectibleDefinition(collectibleHash);

            dynamic jsonDynamic = JsonConvert.DeserializeObject(record);
            if (jsonDynamic == null)
            {
                throw new Exception($"unexpected null result for {nameof(this.worldSqlContent.GetDestinyCollectibleDefinition)} id {collectibleHash}");
            }

            var destinyCollectibleDefinition = new DestinyCollectibleDefinition
            {
                HashId = jsonDynamic.hash,
                ItemHash = jsonDynamic.itemHash,
                Name = jsonDynamic.displayProperties.name,
                IconPath = jsonDynamic.displayProperties.icon,
            };

            if (this.providerOptions.EnableCaching)
            {
                this.destinyCollectibleDefinitionCache.Add(collectibleHash, destinyCollectibleDefinition);
            }

            return destinyCollectibleDefinition;
        }

        public IList<SearchableWeaponRecord> GetSearchableWeapons() => this.worldSqlContent.GetSearchableWeapons();

        public WeaponStatGroupDefinition GetWeaponStatGroupDefinition(uint statGroupHashId)
        {
            var definition = new WeaponStatGroupDefinition
            {
                StatGroupHashId = statGroupHashId,
                InterpolationDefinitions = new List<WeaponStatInterpolationDefinition>(),
            };

            var statGroupDefinitionRecord = this.worldSqlContent.GetDestinyStatGroupDefinition(statGroupHashId);
            dynamic statGroupDefinitionDynamic = JsonConvert.DeserializeObject(statGroupDefinitionRecord);

            foreach (var scaledStatDynamic in statGroupDefinitionDynamic.scaledStats)
            {
                var interpolationDefinition = new WeaponStatInterpolationDefinition
                {
                    StatHashId = scaledStatDynamic.statHash,
                    MaxValue = scaledStatDynamic.maximumValue,
                    DataPoints = new List<Tuple<double, double>>(),
                };

                foreach (var interpolationDynamic in scaledStatDynamic.displayInterpolation)
                {
                    double x = interpolationDynamic.value;
                    double y = interpolationDynamic.weight;

                    interpolationDefinition.DataPoints.Add(new Tuple<double, double>(x, y));
                }

                definition.InterpolationDefinitions.Add(interpolationDefinition);
            }

            return definition;
        }

        internal List<WeaponPerkDefinition> GetWeaponDefinitionPerks(uint plugSetHash)
        {
            var perks = new List<WeaponPerkDefinition>();

            var plugSetDefinitionRecord = this.worldSqlContent.GetDestinyPlugSetDefinition(plugSetHash);
            dynamic plugSetDefinitionDynamic = JsonConvert.DeserializeObject(plugSetDefinitionRecord);
            foreach (var plug in plugSetDefinitionDynamic.reusablePlugItems)
            {
                var perk = this.GetWeaponDefinitionPerk((uint)plug.plugItemHash);
                perks.Add(perk);
            }

            return perks;
        }

        internal WeaponPerkDefinition GetWeaponDefinitionPerk(uint plugItemHash)
        {
            if (this.providerOptions.EnableCaching && this.weaponDefinitionPerkCache.TryGetValue(plugItemHash, out var cachedRecord))
            {
                return cachedRecord;
            }

            var perkRecord = this.worldSqlContent.GetDestinyInventoryItemDefinition(plugItemHash);
            dynamic perkDynamic = JsonConvert.DeserializeObject(perkRecord);

            dynamic perkValuesDynamic = perkDynamic.investmentStats;

            var weaponPerkList = new List<WeaponPerkValueDefinition>();
            foreach (var perkValueDynamic in perkValuesDynamic)
            {
                var perkValue = new WeaponPerkValueDefinition
                {
                    StatHash = perkValueDynamic.statTypeHash,
                    Value = perkValueDynamic.value,
                };

                weaponPerkList.Add(perkValue);
            }

            var perk = new WeaponPerkDefinition
            {
                MetaData = new WeaponPerkMetaData
                {
                    HashId = plugItemHash,
                    Name = perkDynamic.displayProperties.name,
                    Description = perkDynamic.displayProperties.description,
                    IconPath = perkDynamic.displayProperties.icon,
                },
                WeaponPerkValueList = weaponPerkList.Any() ? weaponPerkList : null, // some perks may not have values that affect stats (example: Rampage). but others will (example: Field Prep).
            };

            if (this.providerOptions.EnableCaching)
            {
                this.weaponDefinitionPerkCache.Add(plugItemHash, perk);
            }

            return perk;
        }
    }
}
