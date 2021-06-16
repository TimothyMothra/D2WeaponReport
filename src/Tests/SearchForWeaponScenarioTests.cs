namespace Tests
{
    using System.Linq;

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
                Icon = null,
                CollectibleHash = 1683333367u,
                ItemTypeDisplayName = "3",
            };

            record.Should().BeEquivalentTo(expected);
        }
    }
}
