namespace Tests
{
    using System.IO;

    using DestinyLib;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EnvironmentTests
    {
        /// <summary>
        /// This test checks that the local environment has been initialized. 
        /// If this test fails, run the initialize environment script.
        /// </summary>
        [TestMethod]
        public void VerifyRootMarker()
        {
            var filePath = LibEnvironment.GetDatabaseFilePath("world_sql_content");
            Assert.IsTrue(File.Exists(filePath));
        }
    }
}
