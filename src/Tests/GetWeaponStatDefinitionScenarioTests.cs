namespace Tests
{
    using DestinyLib.DataContract.Definitions;
    using DestinyLib.Scenarios;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetWeaponStatDefinitionScenarioTests
    {
        [TestMethod]
        public void VerifyGet()
        {
            uint id_stability = 155624089;

            var result = GetWeaponStatDefinitionScenario.Run(id_stability);

            var expected = new WeaponStatMetaData
            {
                HashId = 155624089,
                Name = "Stability",
                Description = "How much or little recoil you will experience while firing the weapon.",
                Interpolate = false,
            };

            result.Should().BeEquivalentTo(expected);
        }
    }
}
