﻿namespace Tests
{
    using DestinyLib.DataContract;
    using DestinyLib.Scenarios;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System.Linq;

    [TestClass]
    public class GetWeaponDefinitionScenarioTests
    {
        [TestMethod]
        public void VerifyGet()
        {
            uint id_gnawingHunger = 821154603;

            var result = GetWeaponDefinitionScenario.Run(id_gnawingHunger);

            var expected = ExampleRecords.GetGnawingHunger();

            // This isn't necessary, but is easier to debug.
            //VerifyPerks(expected, result);

            result.Should().BeEquivalentTo(expected);
        }

        private void VerifyPerks(WeaponDefinition expected, WeaponDefinition actual)
        {
            var expectedPerks = expected.PerkSets.SelectMany(x => x.Perks).OrderBy(x => x.Name).ToList();
            var actualPerks = actual.PerkSets.SelectMany(x => x.Perks).OrderBy(x => x.Name).ToList();

            actualPerks.Should().BeEquivalentTo(expectedPerks);
        }
    }
}
