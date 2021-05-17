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
                Name = "Attack",
                Description = "Higher Attack allows your weapons to damage higher-level opponents.",
                StatHash = 1480404414,
                Value = 0,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Power",
                Description = "Raising your Power increases the damage your abilities deal against higher-level enemies.",
                StatHash = 1935470627,
                Value = 0,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "", // TODO: WHAT IS THIS?
                Description = "",
                StatHash = 1885944937,
                Value = 0,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Zoom",
                Description = "How much the weapon's scope can zoom in on targets.",
                StatHash = 3555269338,
                Value = 16,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Rounds Per Minute",
                Description = "The number of shots per minute this weapon can fire.",
                StatHash = 4284893193,
                Value = 600,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Impact",
                Description = "Increases the damage inflicted by each round.",
                StatHash = 4043523819,
                Value = 21,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Range",
                Description = "Increases the effective range of this weapon.",
                StatHash = 1240592695,
                Value = 53,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Stability",
                Description = "How much or little recoil you will experience while firing the weapon.",
                StatHash = 155624089,
                Value = 49,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Magazine",
                Description = "The number of shots which can be fired before reloading.",
                StatHash = 3871231066,
                Value = 43,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Reload Speed",
                Description = "The time it takes to reload this weapon.",
                StatHash = 4188031367,
                Value = 61,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Inventory Size",
                Description = "How much ammo a player can hold in reserve.",
                StatHash = 1931675084,
                Value = 55,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Handling",
                Description = "The speed with which the weapon can be readied and aimed.",
                StatHash = 943549884,
                Value = 67,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Aim Assistance",
                Description = "The weapon's ability to augment your aim.",
                StatHash = 1345609583,
                Value = 65,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Recoil Direction",
                Description = "The weapon's tendency to move while firing.",
                StatHash = 2715839340,
                Value = 54,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });

            #endregion

            #region Perks
            gnawingHunger.PerkSets.Add(new WeaponDefinition.PerkSet
            {
                SocketIndex = 1,
                SocketTypeHash = 3362409147,
                PlugSetHash = 295878355,
                //Perks = new List<WeaponDefinition.Perk>
                //{
                //    new WeaponDefinition.Perk
                //    {

                //    },
                //}
            });
            gnawingHunger.PerkSets.Add(new WeaponDefinition.PerkSet
            {
                SocketIndex = 2,
                SocketTypeHash = 3815406785,
                PlugSetHash = 3964805173,
            });
            gnawingHunger.PerkSets.Add(new WeaponDefinition.PerkSet
            {
                SocketIndex = 3,
                SocketTypeHash = 2614797986,
                PlugSetHash = 2297212861,
            });
            gnawingHunger.PerkSets.Add(new WeaponDefinition.PerkSet
            {
                SocketIndex = 4,
                SocketTypeHash = 2614797986,
                PlugSetHash = 1853656119,
            });


            #endregion



            return gnawingHunger;
        }
    }
}
