namespace Tests.StressTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using BungieLib.Manifest;

    using DestinyLib;
    using DestinyLib.Database;
    using DestinyLib.Operations;
    using DestinyLib.Scenarios;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    //[Ignore] // DISABLE LOCALLY. // TODO: CONVERT TO PARAM-BASED PRE-PROCESSOR https://stackoverflow.com/questions/43836548/define-c-sharp-preprocessor-from-msbuild/51782647
    [TestClass]
    public class StressTests
    {
        private readonly WorldSqlContent worldSqlContent;

        public StressTests()
        {
            var dbPath = new FileInfo(LibEnvironment.GetDatabaseFilePath("world_sql_content"));
            this.worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));
        }

        /// <summary>
        /// This test will get all weapons and attempt to parse them.
        /// If any test fails, likely something breaks the expected data schema.
        /// </summary>
        [TestMethod]
        public void StressTest_CanParseWeapons()
        {
            var worldSqlContentProvider = new WorldSqlContentProvider(this.worldSqlContent, ProviderOptions.TestWithCaching);

            var weapons = worldSqlContentProvider.GetSearchableWeapons();

            var failedIds = new List<string>();

            foreach (var weapon in weapons)
            {
                try
                {
                    _ = worldSqlContentProvider.GetWeaponDefinition(weapon.HashId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{weapon.HashId} {weapon.Name}");
                    Console.WriteLine(ex.Message);

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
        public void StressTest_Analysis()
        {
            // TODO, NEED TO PASS IN MY OWN PROVIDER.
            var worldSqlContentProvider = new WorldSqlContentProvider(this.worldSqlContent, ProviderOptions.TestWithCaching);

            var weapons = worldSqlContentProvider.GetSearchableWeapons();

            var failedIds = new List<string>();

            foreach (var weapon in weapons)
            {
                try
                {
                    var result = GetWeaponAnalysisScenario.Run(weapon.HashId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{weapon.HashId} {weapon.Name}");
                    Console.WriteLine(ex.Message);

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
        public void StressTest_StatPermutationPercentiles()
        {
            var worldSqlContentProvider = new WorldSqlContentProvider(this.worldSqlContent, ProviderOptions.TestWithCaching);

            var weapons = worldSqlContentProvider.GetSearchableWeapons();

            var failedIds = new List<string>();

            foreach (var weapon in weapons)
            {
                try
                {
                    var weaponDefinition = worldSqlContentProvider.GetWeaponDefinition(weapon.HashId);
                    _ = WeaponAnalysisGenerator.GetStatPermutationPercentiles(weaponDefinition);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{weapon.HashId} {weapon.Name}");
                    Console.WriteLine(ex.Message);

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
