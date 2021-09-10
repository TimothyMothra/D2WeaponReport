namespace Tests.Operations
{
    using System.Collections.Generic;

    using DestinyLib.DataContract.Definitions;
    using DestinyLib.Operations;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using static DestinyLib.Operations.PerkPermutationGenerator;

    [TestClass]
    public class PerkPermutationGeneratorTests
    {
        [TestMethod]
        public void Verify_BehaviorIncludePerksWithNoValue_False()
        {
            var exampleWeaponPerkSetCollection = this.GetExampleRecord();

            var result = PerkPermutationGenerator.GetPerkPermutations(
                weaponPerkSetCollection: exampleWeaponPerkSetCollection,
                options: new Options { BehaviorIncludePerksWithNoValue = false });

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Verify_BehaviorIncludePerksWithNoValue_True()
        {
            var exampleWeaponPerkSetCollection = this.GetExampleRecord();

            var result = PerkPermutationGenerator.GetPerkPermutations(
                weaponPerkSetCollection: exampleWeaponPerkSetCollection,
                options: new Options { BehaviorIncludePerksWithNoValue = true });

            Assert.AreEqual(2, result.Count);
        }

        private WeaponPerkSetCollection GetExampleRecord()
        {
            var weaponPerkSetCollection = new WeaponPerkSetCollection();

            weaponPerkSetCollection.Values.Add(new WeaponPerkSetDefinition
            {
                Values = new List<WeaponPerkDefinition>
                {
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData { HashId = 111, Name = "Ones", },
                        WeaponPerkValueList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition { StatHash = 111, Value = 10, },
                            new WeaponPerkValueDefinition { StatHash = 222, Value = 10, },
                        },
                    }, new WeaponPerkDefinition
                    {
                       MetaData = new WeaponPerkMetaData { HashId = 222, Name = "Twos", },
                       WeaponPerkValueList = null,
                    },
                },
            });

            weaponPerkSetCollection.Values.Add(new WeaponPerkSetDefinition
            {
                Values = new List<WeaponPerkDefinition>
                {
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData { HashId = 333, Name = "Threes", },
                        WeaponPerkValueList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition { StatHash = 111, Value = 5, },
                            new WeaponPerkValueDefinition { StatHash = 222, Value = -5, },
                        },
                    },
                },
            });

            return weaponPerkSetCollection;
        }
    }
}
