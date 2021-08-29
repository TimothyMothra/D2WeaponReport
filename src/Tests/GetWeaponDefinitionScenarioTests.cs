namespace Tests
{
    using System.Linq;

    using DestinyLib.DataContract;
    using DestinyLib.DataContract.Definitions;
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

        private void VerifyPerks(WeaponDefinition expected, WeaponDefinition actual)
        {
            var expectedPerks = expected.WeaponPossiblePerks.Values.SelectMany(x => x.Values).OrderBy(x => x.MetaData.Name).ToList();
            var actualPerks = actual.WeaponPossiblePerks.Values.SelectMany(x => x.Values).OrderBy(x => x.MetaData.Name).ToList();

            actualPerks.Should().BeEquivalentTo(expectedPerks);
        }
    }
}
