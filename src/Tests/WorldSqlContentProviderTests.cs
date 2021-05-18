namespace Tests
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using DestinyLib;
    using DestinyLib.Database;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Newtonsoft.Json;

    [TestClass]
    public class WorldSqlContentProviderTests
    {
        private readonly WorldSqlContent WorldSqlContent;
        private readonly WorldSqlContentProvider WorldSqlContentProvider;

        public WorldSqlContentProviderTests()
        {
            var dbPath = LibEnvironment.GetDatabaseFile("world_sql_content");
            this.WorldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));
            this.WorldSqlContentProvider = new WorldSqlContentProvider(this.WorldSqlContent, ProviderOptions.ScenarioDefault);
        }

        [TestMethod]
        public void TestGetWeapon_DoubleEdgedAnswer()
        {
            uint id_doubleEdgedAnswer = 3551104348;

            var weaponDefinition = this.WorldSqlContentProvider.GetWeaponDefinition(id_doubleEdgedAnswer);

            Assert.AreEqual("Double-Edged Answer", weaponDefinition.MetaData.Name);
        }

        [TestMethod]
        public void TestGetWeapon_TicuusDivination()
        {
            uint id_doubleEdgedAnswer = 3260753130;

            var weaponDefinition = this.WorldSqlContentProvider.GetWeaponDefinition(id_doubleEdgedAnswer);

            Assert.AreEqual("Ticuu's Divination", weaponDefinition.MetaData.Name);
        }

        [TestMethod]
        public void TestGetWeapon_GnawingHunger()
        {
            uint id_gnawingHunger = 821154603;

            var weaponDefiniton = this.WorldSqlContentProvider.GetWeaponDefinition(id_gnawingHunger);

            var expected = ExampleRecords.GetGnawingHunger();

            weaponDefiniton.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void TestGetSearchableWeapons()
        {
            var weapons = this.WorldSqlContentProvider.GetSearchableWeapons();
            Assert.IsTrue(weapons.Any());
            Assert.IsTrue(weapons.Count > 800);
        }

        [TestMethod]
        public void TestGetWeaponPerk_ChamberedCompensator()
        {
            // CHAMBERED COMPENSATOR is the first perk i've found that hits the uint vs int problem.
            // id = -633580228
            // hash = 3661387068

            uint hashId = 3661387068;
            int id = unchecked((int)hashId);

            var record = this.WorldSqlContent.GetDestinyInventoryItemDefinition(hashId);
            dynamic jsonDynamic = JsonConvert.DeserializeObject(record);
            string name = jsonDynamic.displayProperties.name;

            Assert.AreEqual("Chambered Compensator", name);
        }

        /// <summary>
        /// This test will get all weapons and attempt to parse them.
        /// If any test fails, likely something breaks the expected data schema.
        /// </summary>
        [TestMethod]
        public void TestCanParseWeapons()
        {
            var weapons = this.WorldSqlContentProvider.GetSearchableWeapons();

            var failedIds = new List<string>();

            foreach(var weapon in weapons)
            {
                try
                {
                    _ = this.WorldSqlContentProvider.GetWeaponDefinition(weapon.HashId);
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
