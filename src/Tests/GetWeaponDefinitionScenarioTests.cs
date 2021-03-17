namespace Tests
{
    using System.Collections.Generic;

    using DestinyLib.DataContract;
    using DestinyLib.Scenarios;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetWeaponDefinitionScenarioTests
    {
        [TestMethod]
        public void VerifyGet()
        {
            long id_gnawingHunger = 821154603;

            var result = GetWeaponDefinitionScenario.Run(id_gnawingHunger);

            var expected = new WeaponDefinition
            {
                MetaData = new WeaponDefinition.WeaponMetaData
                {
                    Id = 821154603,
                    Name = "Gnawing Hunger",
                    AmmoTypeName = "1",
                    TierTypeName = "Legendary",
                    DefaultDamageTypeHash = "3454344768",
                    FlavorText = "Don't let pride keep you from a good meal.",
                },
                Stats = new List<WeaponDefinition.WeaponStat>(),
                PerkSets = new List<WeaponDefinition.PerkSet>(),
            };

            result.Should().BeEquivalentTo(expected);
        }
    }
}
