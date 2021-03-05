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
        /// TODO: WRITE THE INITIALIZE ENVIRONMENT SCRIPT :)
        /// </summary>
        [TestMethod]
        public void VerifyRootMarker()
        {
            var filePath = Environment.GetDatabaseFile("world_sql_content");
            Assert.IsTrue(File.Exists(filePath));
        }
    }
}
