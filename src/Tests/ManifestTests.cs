namespace Tests
{
    using System.Threading.Tasks;

    using DestinyLib;
    using DestinyLib.Api;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ManifestTests
    {
        [TestMethod]
        public async Task VerifyGetWorldSqlContent()
        {
            var manifest = new Manifest();
            var uri = await manifest.GetWorldSqlContentUri();

            // the result will look like "world_sql_content_<GUID>.content".
            // the GUID value will change as Bungie updates the database.
            var value = uri.AbsoluteUri;
            Assert.IsTrue(value.StartsWith("https://www.bungie.net/common/destiny2_content/sqlite/en/world_sql_content_"));
            Assert.IsTrue(value.EndsWith(".content"));
        }
    }
}
