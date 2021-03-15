namespace Tests
{
    using System.Linq;

    using DestinyLib;
    using DestinyLib.Analysis;
    using DestinyLib.Database;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AnalysisControllerTests
    {
        private readonly AnalysisController AnalysisController;

        public AnalysisControllerTests()
        {
            var dbPath = LibEnvironment.GetDatabaseFile("world_sql_content");
            var worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath););

            this.AnalysisController = new AnalysisController(worldSqlContent);
        }

        [TestMethod]
        public void VerifySearch_StringContains()
        {
            var results = this.AnalysisController.Search("gn", AnalysisController.SearchType.StringContains);

            Assert.IsTrue(results.Any());
        }


        [TestMethod]
        public void VerifySearch_Regex()
        {
            var results = this.AnalysisController.Search("gnw", AnalysisController.SearchType.Regex);

            Assert.IsTrue(results.Any());
        }
    }
}
