namespace Tests
{
    using System.Linq;

    using DestinyLib.Scenarios;

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
    }
}
