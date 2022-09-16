namespace Tests.Database
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using BungieLib.Manifest;
    using DestinyLib;
    using DestinyLib.Database;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Newtonsoft.Json;

    [TestClass]
    public class WorldSqlContentProviderTests
    {
        private readonly WorldSqlContent worldSqlContent;
        private readonly WorldSqlContentProvider worldSqlContentProvider;

        public WorldSqlContentProviderTests()
        {
            var dbPath = new FileInfo(LibEnvironment.GetDatabaseFilePath("world_sql_content"));
            this.worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));

            this.worldSqlContentProvider = new WorldSqlContentProvider(this.worldSqlContent, ProviderOptions.TestWithCaching);
        }

        [TestMethod]
        public void TestGetWeapon_DoubleEdgedAnswer()
        {
            uint id_doubleEdgedAnswer = 3551104348;

            var weaponDefinition = this.worldSqlContentProvider.GetWeaponDefinition(id_doubleEdgedAnswer);

            Assert.AreEqual("Double-Edged Answer", weaponDefinition.MetaData.Name);
        }

        [TestMethod]
        public void TestGetWeapon_TicuusDivination()
        {
            uint id_doubleEdgedAnswer = 3260753130;

            var weaponDefinition = this.worldSqlContentProvider.GetWeaponDefinition(id_doubleEdgedAnswer);

            Assert.AreEqual("Ticuu's Divination", weaponDefinition.MetaData.Name);
        }

        [TestMethod]
        public void TestGetWeapon_GnawingHunger()
        {
            uint id_gnawingHunger = 821154603;

            var weaponDefiniton = this.worldSqlContentProvider.GetWeaponDefinition(id_gnawingHunger);

            var expected = ExampleRecords.GetGnawingHunger();

            weaponDefiniton.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void TestGetSearchableWeapons()
        {
            var weapons = this.worldSqlContentProvider.GetSearchableWeapons();
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
            Debug.WriteLine($"hash: {hashId}; uint: {id}");

            var record = this.worldSqlContent.GetDestinyInventoryItemDefinition(hashId);
            dynamic jsonDynamic = JsonConvert.DeserializeObject(record);
            string name = jsonDynamic.displayProperties.name;

            Assert.AreEqual("Chambered Compensator", name);
        }

        [TestMethod]
        public void TestGetStatGroupDefinition_GnawingHunger()
        {
            uint id_GnawingHungerStatGroup = 3941551777;

            var statGroupDefinition = this.worldSqlContentProvider.GetWeaponStatGroupDefinition(id_GnawingHungerStatGroup);

            var expectedRecord = ExampleRecords.GetGnawingHunger_StatGroupDefinition();

            Assert.AreEqual(11, statGroupDefinition.InterpolationDefinitions.Count);

            statGroupDefinition.Should().BeEquivalentTo(expectedRecord);
        }
    }
}
