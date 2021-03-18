namespace Tests
{
    using System.Collections.Generic;

    using DestinyLib.DataContract;

    public static class ExampleRecords
    {
        /// <summary>
        /// (https://data.destinysets.com/i/InventoryItem:821154603).
        /// </summary>
        public static WeaponDefinition GetGnawingHunger()
        {
            var gnawingHunger = new WeaponDefinition
            {
                MetaData = new WeaponDefinition.WeaponMetaData
                {
                    Id = 821154603,
                    Name = "Gnawing Hunger",
                    AmmoTypeId = "1",
                    TierTypeName = "Legendary",
                    DefaultDamageTypeId = "4",
                    DefaultDamageTypeHash = "3454344768",
                    FlavorText = "Don't let pride keep you from a good meal.",
                    ItemTypeId = "6"
                },
                Stats = new List<WeaponDefinition.WeaponStat>(),
                PerkSets = new List<WeaponDefinition.PerkSet>(),
            };

            #region Stats
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "xxx", // TODO: This is in the DestinyStatDefinition table.
                Description = "xxx", // TODO: This is in the DestinyStatDefinition table.
                StatHash = "1480404414",
                Value = 0,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "xxx", // TODO: This is in the DestinyStatDefinition table.
                Description = "xxx", // TODO: This is in the DestinyStatDefinition table.
                StatHash = "1935470627",
                Value = 0,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "xxx", // TODO: This is in the DestinyStatDefinition table.
                Description = "xxx", // TODO: This is in the DestinyStatDefinition table.
                StatHash = "1885944937",
                Value = 0,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "xxx", // TODO: This is in the DestinyStatDefinition table.
                Description = "xxx", // TODO: This is in the DestinyStatDefinition table.
                StatHash = "3555269338",
                Value = 16,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "xxx", // TODO: This is in the DestinyStatDefinition table.
                Description = "xxx", // TODO: This is in the DestinyStatDefinition table.
                StatHash = "4284893193",
                Value = 600,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "xxx", // TODO: This is in the DestinyStatDefinition table.
                Description = "xxx", // TODO: This is in the DestinyStatDefinition table.
                StatHash = "4043523819",
                Value = 21,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "xxx", // TODO: This is in the DestinyStatDefinition table.
                Description = "xxx", // TODO: This is in the DestinyStatDefinition table.
                StatHash = "1240592695",
                Value = 53,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "xxx", // TODO: This is in the DestinyStatDefinition table.
                Description = "xxx", // TODO: This is in the DestinyStatDefinition table.
                StatHash = "155624089",
                Value = 49,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "xxx", // TODO: This is in the DestinyStatDefinition table.
                Description = "xxx", // TODO: This is in the DestinyStatDefinition table.
                StatHash = "3871231066",
                Value = 43,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "xxx", // TODO: This is in the DestinyStatDefinition table.
                Description = "xxx", // TODO: This is in the DestinyStatDefinition table.
                StatHash = "4188031367",
                Value = 61,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "xxx", // TODO: This is in the DestinyStatDefinition table.
                Description = "xxx", // TODO: This is in the DestinyStatDefinition table.
                StatHash = "1931675084",
                Value = 55,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "xxx", // TODO: This is in the DestinyStatDefinition table.
                Description = "xxx", // TODO: This is in the DestinyStatDefinition table.
                StatHash = "943549884",
                Value = 67,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "xxx", // TODO: This is in the DestinyStatDefinition table.
                Description = "xxx", // TODO: This is in the DestinyStatDefinition table.
                StatHash = "1345609583",
                Value = 65,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "xxx", // TODO: This is in the DestinyStatDefinition table.
                Description = "xxx", // TODO: This is in the DestinyStatDefinition table.
                StatHash = "2715839340",
                Value = 54,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });

            #endregion

            return gnawingHunger;
        }
    }
}
