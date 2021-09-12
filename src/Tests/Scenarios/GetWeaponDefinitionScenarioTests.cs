namespace Tests.Scenarios
{
    using System.Collections.Generic;
    using System.Linq;

    using DestinyLib.DataContract;
    using DestinyLib.DataContract.Analysis;
    using DestinyLib.DataContract.Definitions;
    using DestinyLib.Extensions;
    using DestinyLib.Operations;
    using DestinyLib.Scenarios;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [TestMethod]
        public void TestStatPermutations()
        {
            uint id_gnawingHunger = 821154603;
            var weaponDefinition = GetWeaponDefinitionScenario.Run(id_gnawingHunger);

            var perkPermutations = PerkPermutationGenerator.GetPerkPermutations(weaponDefinition.WeaponPossiblePerks);
            var statPermutations = perkPermutations.Select(x => new StatPermutation(x)).ToList();

            Assert.IsNotNull(statPermutations);

            var statsDictionary = new Dictionary<uint, List<double>>();
            foreach (var sp in statPermutations)
            {
                foreach (var kvp in sp.PerkHashAndValues)
                {
                    statsDictionary.CustomAdd(kvp.Key, kvp.Value);
                }
            }

            statsDictionary.ToString();

            ///

            var statDefinitions = weaponDefinition.WeaponBaseStats.Values;
            var metaDataList = statDefinitions.Select(x => x.MetaData).ToList();

            ///

            var statPermutationPercentiles = new List<StatPermutationPercentiles>();

            //var ids = statsDictionary.Select(x => x.Key).ToList();

            foreach (var sd in statsDictionary)
            {
                var name = metaDataList.Single(x => x.HashId == sd.Key).Name;
                statPermutationPercentiles.Add(new StatPermutationPercentiles(name: name, hashId: sd.Key, values: sd.Value));
            }

            statPermutationPercentiles.ToList();
        }

        private static void VerifyPerks(WeaponDefinition expected, WeaponDefinition actual)
        {
            var expectedPerks = expected.WeaponPossiblePerks.Values.SelectMany(x => x.Values).OrderBy(x => x.MetaData.Name).ToList();
            var actualPerks = actual.WeaponPossiblePerks.Values.SelectMany(x => x.Values).OrderBy(x => x.MetaData.Name).ToList();

            actualPerks.Should().BeEquivalentTo(expectedPerks);
        }
    }
}
