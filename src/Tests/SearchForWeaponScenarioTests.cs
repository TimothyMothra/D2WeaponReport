namespace Tests
{
    using System.Linq;

    using DestinyLib;
    using DestinyLib.Scenarios;
    using DestinyLib.Database;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SearchForWeaponScenarioTests
    {
        private readonly SearchForWeaponScenario SearchForWeaponScenario;

        public SearchForWeaponScenarioTests()
        {
            var dbPath = LibEnvironment.GetDatabaseFile("world_sql_content");
            var worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));

            this.SearchForWeaponScenario = new SearchForWeaponScenario(worldSqlContent);
        }

        [TestMethod]
        public void VerifySearch_StringContains()
        {
            var results = this.SearchForWeaponScenario.Run("gn", SearchForWeaponScenario.SearchType.StringContains);

            Assert.IsTrue(results.Any());
        }


        [TestMethod]
        public void VerifySearch_Regex()
        {
            var results = this.SearchForWeaponScenario.Run("gnw", SearchForWeaponScenario.SearchType.Regex);

            Assert.IsTrue(results.Any());
        }
    }
}
