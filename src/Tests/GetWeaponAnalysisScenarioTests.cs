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
            uint id_deadmanstale = 3654674561;
            //uint id_xenoclast = 2891672170;
            //uint id_gnawingHunger = 821154603;
            //uint id_chromaRush = 1119734784;
            //uint id_crownSplitter = 741454304;
            uint id_arsenicBite = 720351795;

            //var summary = GetWeaponAnalysisScenario.Run(id_deadmanstale);
            var summary = GetWeaponAnalysisScenario.Run(id_arsenicBite);
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
