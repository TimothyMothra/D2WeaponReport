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
            uint id_xenoclast = 2891672170;
            //uint id_gnawingHunger = 821154603;
            //uint id_chromaRush = 1119734784;
            //uint id_crownSplitter = 741454304;

            var summary = GetWeaponAnalysisScenario.Run(id_xenoclast);
        }
    }
}
