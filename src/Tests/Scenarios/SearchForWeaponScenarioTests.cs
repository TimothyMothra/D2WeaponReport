namespace Tests.Scenarios
{
    using System.Linq;

    using DestinyLib.Database.DataContract.Definitions;
    using DestinyLib.DataContract;
    using DestinyLib.Scenarios;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SearchForWeaponScenarioTests
    {
        [TestMethod]
        public void VerifySearch_StringContains()
        {
            var results = SearchForWeaponScenario.Run("gn", SearchForWeaponScenario.SearchType.StringContains);

            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public void VerifySearch_Regex()
        {
            var results = SearchForWeaponScenario.Run("gnw", SearchForWeaponScenario.SearchType.Regex);

            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public void VerifySearchRecord()
        {
            var results = SearchForWeaponScenario.Run("gnawing hunger", SearchForWeaponScenario.SearchType.Regex);
            var record = results.Single();

            var expected = new SearchableWeaponRecord
            {
                Name = "Gnawing Hunger",
                Id = 821154603,
                HashId = 821154603u,
                CollectionDefintitionIconPath = "/common/destiny2_content/icons/48037e6416c3c9da07030a72931e0ca9.jpg",
                ItemDefinitionIconPath = "/common/destiny2_content/icons/c4509acb76551495deac51bb29b61248.jpg",
                CollectibleHash = 1683333367u,
                ItemTypeDisplayName = "Auto Rifle",
            };

            record.Should().BeEquivalentTo(expected);
        }
    }
}
