namespace Tests
{
    using DestinyLib.Scenarios;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetWeaponAnalysisScenarioTests
    {
        [TestMethod]
        public void VerifyGet()
        {
            //uint id = 3654674561; // DeadMansTale
            //uint id = 2891672170; // Xenoclast
            uint id = 821154603; // GnawingHunger
            //uint id = 1119734784; // ChromaRush
            //uint id = 741454304; // CrownSplitter
            //uint id = 720351795; // ArsenicBite

            var summary = GetWeaponAnalysisScenario.Run(id);
            Assert.IsNotNull(summary);
        }

        [TestMethod]
        public void VerifyGet_EmptyPerks()
        {
            uint id_legendofacrius = 1744115122; 
            var summary = GetWeaponAnalysisScenario.Run(id_legendofacrius);
            Assert.IsTrue(summary.HasEmptyPerks);
        }
    }
}
