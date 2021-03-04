namespace Tests
{
    using DestinyLib.Database;
    using DestinyLib.DataContract;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var dbPath = "C:\\TimothyMothra\\environment\\world_sql_content_30e996ad1e317a77a5130f587198da50.content";
            var connStr = $"Data Source={dbPath}";

            var data = new WorldSqlContent(connectionString: connStr);
            var actual = data.GetWeaponItemDefinition(id: 821154603);

            var expected = new WeaponItemDefinition 
            { 
                DefaultDamageTypeHash = 3454344768,
                Name = "Gnawing Hunger",
                AmmoType = 1,
                TierTypeName = "Legendary",
            };

            expected.Should().BeEquivalentTo(actual);
        }

        [TestMethod]
        public void VerifyRootMarker()
        {
            var path = "C:\\TimothyMothra\\src\\root.marker";

            Assert.Inconclusive();
        }
    }
}
