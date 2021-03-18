namespace Tests
{
    using System.Collections.Generic;

    using DestinyLib.DataContract;

    public static class ExampleRecords
    {
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
                },
                Stats = new List<WeaponDefinition.WeaponStat>(),
                PerkSets = new List<WeaponDefinition.PerkSet>(),
            };

            return gnawingHunger;
        }
    }
}
