namespace Tests
{
    using DestinyLib.Database;

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
            var weapon = data.GetSingleWeapon(id: 821154603);

            Assert.AreEqual("Gnawing Hunger", weapon.Name);
            Assert.AreEqual(3454344768, weapon.DefaultDamageTypeHash);
        }

        [TestMethod]
        public void VerifyRootMarker()
        {
            var path = "C:\\TimothyMothra\\src\\root.marker";

            Assert.Inconclusive();
        }
    }
}
