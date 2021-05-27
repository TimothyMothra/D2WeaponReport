namespace Tests
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using DestinyLib;
    using DestinyLib.Database;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var worldSqlContentProvider = new WorldSqlContentProvider(this.WorldSqlContent, new ProviderOptions { EnableCaching = true });

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
    }
}
