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
        public void TestGetWeapon()
        {
            var id_gnawingHunger = 821154603;

            var weaponDefiniton = this.WorldSqlContentProvider.GetWeaponDefinition(id_gnawingHunger);

            var expected = new WeaponDefinition
            {
                MetaData = new WeaponDefinition.WeaponMetaData
                {
                    Id = 821154603,
                    Name = "Gnawing Hunger",
                    AmmoTypeName = "1",
                    TierTypeName = "Legendary",
                    DefaultDamageTypeHash = "3454344768",
                    FlavorText = "Don't let pride keep you from a good meal.",
                },
                Stats = new List<WeaponDefinition.WeaponStat>(),
                PerkSets = new List<WeaponDefinition.PerkSet>(),
            };

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
