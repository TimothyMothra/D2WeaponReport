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
            var dbPath = Environment.GetDatabaseFile("world_sql_content");
            var connStr = $"Data Source={dbPath}";
            var worldSqlContent = new WorldSqlContent(connectionString: connStr);

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
