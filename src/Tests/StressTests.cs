namespace Tests
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using DestinyLib;
    using DestinyLib.Database;
    using DestinyLib.Scenarios;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    //[Ignore]
    [TestClass]
    public class StressTests
    {
        private readonly WorldSqlContent WorldSqlContent;

        public StressTests()
        {
            var dbPath = new FileInfo(LibEnvironment.GetDatabaseFilePath("world_sql_content"));
            this.WorldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));
        }

        /// <summary>
        /// This test will get all weapons and attempt to parse them.
        /// If any test fails, likely something breaks the expected data schema.
        /// </summary>
        //[Ignore] // DISABLE LOCALLY. // TODO: CONVERT TO PARAM-BASED PRE-PROCESSOR https://stackoverflow.com/questions/43836548/define-c-sharp-preprocessor-from-msbuild/51782647
        [TestMethod]
        public void TestCanParseWeapons()
        {
            var worldSqlContentProvider = new WorldSqlContentProvider(this.WorldSqlContent, ProviderOptions.TestWithCaching );

            var weapons = worldSqlContentProvider.GetSearchableWeapons();

            var failedIds = new List<string>();

            foreach (var weapon in weapons)
            {
                try
                {
                    _ = worldSqlContentProvider.GetWeaponDefinition(weapon.HashId);
                }
                catch
                {
                    failedIds.Add(weapon.ToString());
                }
            }

            if (failedIds.Any())
            {
                failedIds.ForEach(x => Debug.WriteLine(x));

                Assert.Fail($"Failed to parse {failedIds.Count} weaponsIds");
            }
        }

        [TestMethod]
        public void TestAnalysis()
        {
            /// TODO: the following weapons fail this test:
            /// [1619016919] Khvostov 7G-02 (Auto Rifle)
            /// [1744115122] Legend of Acrius(Shotgun)

            var worldSqlContentProvider = new WorldSqlContentProvider(this.WorldSqlContent, ProviderOptions.TestWithCaching);

            var weapons = worldSqlContentProvider.GetSearchableWeapons();

            var failedIds = new List<string>();

            foreach (var weapon in weapons)
            {
                try
                {

                    // TODO, NEED TO PASS IN MY OWN PROVIDER.
                    _ = GetWeaponAnalysisScenario.Run(weapon.HashId);
                }
                catch
                {
                    failedIds.Add(weapon.ToString());
                }
            }

            if (failedIds.Any())
            {
                failedIds.ForEach(x => Debug.WriteLine(x));

                Assert.Fail($"Failed to parse {failedIds.Count} weaponsIds");
            }
        }
    }
}
