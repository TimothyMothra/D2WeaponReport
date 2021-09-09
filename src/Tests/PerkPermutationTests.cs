namespace Tests
{
    using System.Collections.Generic;

    using DestinyLib.Analysis;
    using DestinyLib.DataContract;
    using DestinyLib.DataContract.Definitions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PerkPermutationTests
    {
        private readonly List<WeaponStatDefinition> exampleWeaponStat = new ()
        {
            new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 111,
                },
                MaxValue = 10,
                MinValue = 0,
                Value = 5,
            },
            new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 222,
                },
                MaxValue = 50,
                MinValue = 0,
                Value = 25,
            },
        };

        private readonly List<WeaponStatDefinition> weirdBowStat = new ()
        {
            // Draw Time on bows seems to ignore the maxValue
            new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 111,
                },
                MaxValue = 100,
                MinValue = 0,
                Value = 651,
            },
            new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 222,
                },
                MaxValue = 50,
                MinValue = 0,
                Value = 25,
            },
        };

        [TestMethod]
        public void ValidateMethod_SinglePerk_IsCorrect()
        {
            var perkPermutation = new PerkPermutationWithMaxPoints
            {
                PerkNames = "test perk",
                PerkHashAndValues = new Dictionary<uint, double> { { 111, 4 }, },
            };

            perkPermutation.Validate(this.exampleWeaponStat);

            Assert.AreEqual(4, perkPermutation.MaxPoints);
        }

        [TestMethod]
        public void ValidateMethod_TwoPerks_IsCorrect()
        {
            var perkPermutation = new PerkPermutationWithMaxPoints
            {
                PerkNames = "test perk",
                PerkHashAndValues = new Dictionary<uint, double> { { 111, 4 }, { 222, 10 }, },
            };

            perkPermutation.Validate(this.exampleWeaponStat);
            Assert.AreEqual(14, perkPermutation.MaxPoints);
        }

        [TestMethod]
        public void ValidateMethod_SinglePerk_CannotExceedStatMaxValue()
        {
            var perkPermutation = new PerkPermutationWithMaxPoints
            {
                PerkNames = "test perk",
                PerkHashAndValues = new Dictionary<uint, double> { { 111, 10 }, },
            };

            perkPermutation.Validate(this.exampleWeaponStat);
            Assert.AreEqual(5, perkPermutation.MaxPoints);
        }

        [TestMethod]
        public void ValidateMethod_SinglePerk_CannotExceedStatMinValue()
        {
            var perkPermutation = new PerkPermutationWithMaxPoints
            {
                PerkNames = "test perk",
                PerkHashAndValues = new Dictionary<uint, double> { { 111, -10 }, },
            };

            perkPermutation.Validate(this.exampleWeaponStat);
            Assert.AreEqual(-5, perkPermutation.MaxPoints);
        }
    }
}
