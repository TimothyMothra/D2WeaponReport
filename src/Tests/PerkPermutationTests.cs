﻿namespace Tests
{
    using System.Collections.Generic;

    using DestinyLib.Analysis;
    using DestinyLib.DataContract;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PerkPermutationTests
    {
        private readonly List<WeaponDefinition.WeaponStat> exampleWeaponStat = new()
        {
            new WeaponDefinition.WeaponStat
            {
                StatHash = 111,
                MaxValue = 10,
                MinValue = 0,
                Value = 5,
            },
            new WeaponDefinition.WeaponStat
            {
                StatHash = 222,
                MaxValue = 50,
                MinValue = 0,
                Value = 25,
            },
        };

        private readonly List<WeaponDefinition.WeaponStat> weirdBowStat = new()
        {
            // Draw Time on bows seems to ignore the maxValue
            new WeaponDefinition.WeaponStat
            {
                StatHash = 111,
                MaxValue = 100,
                MinValue = 0,
                Value = 651,
            },
            new WeaponDefinition.WeaponStat
            {
                StatHash = 222,
                MaxValue = 50,
                MinValue = 0,
                Value = 25,
            },
        };

        [TestMethod]
        public void ValidateMethod_SinglePerk_IsCorrect()
        {
            var perkPermutation = new PerkPermutation
            {
                PerkNames = "test perk",
                PerkHashAndValues = new Dictionary<uint, double> { { 111, 4 }, }
            };

            perkPermutation.Validate(exampleWeaponStat);

            Assert.AreEqual(4, perkPermutation.MaxPoints);
        }

        [TestMethod]
        public void ValidateMethod_TwoPerks_IsCorrect()
        {
            var perkPermutation = new PerkPermutation
            {
                PerkNames = "test perk",
                PerkHashAndValues = new Dictionary<uint, double> { { 111, 4 }, { 222, 10 }, }
            };

            perkPermutation.Validate(exampleWeaponStat);
            Assert.AreEqual(14, perkPermutation.MaxPoints);
        }

        [TestMethod]
        public void ValidateMethod_SinglePerk_CannotExceedStatMaxValue()
        {
            var perkPermutation = new PerkPermutation
            {
                PerkNames = "test perk",
                PerkHashAndValues = new Dictionary<uint, double> { { 111, 10 }, }
            };

            perkPermutation.Validate(exampleWeaponStat);
            Assert.AreEqual(5, perkPermutation.MaxPoints);
        }

        [TestMethod]
        public void ValidateMethod_SinglePerk_CannotExceedStatMinValue()
        {
            var perkPermutation = new PerkPermutation
            {
                PerkNames = "test perk",
                PerkHashAndValues = new Dictionary<uint, double> { { 111, -10 }, }
            };

            perkPermutation.Validate(exampleWeaponStat);
            Assert.AreEqual(-5, perkPermutation.MaxPoints);
        }
    }
}
