namespace Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using DestinyLib;
    using DestinyLib.Database;
    using DestinyLib.DataContract;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WorldSqlContentProviderTests
    {
        private readonly WorldSqlContentProvider WorldSqlContentProvider;

        public WorldSqlContentProviderTests()
        {
            var dbPath = LibEnvironment.GetDatabaseFile("world_sql_content");
            var worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));
            this.WorldSqlContentProvider = new WorldSqlContentProvider(worldSqlContent);
        }

        [TestMethod]
        public void TestGetWeapon_GnawingHunger()
        {
            var id_gnawingHunger = 821154603;

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
    }
}
