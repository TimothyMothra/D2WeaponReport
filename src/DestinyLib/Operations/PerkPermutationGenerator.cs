﻿namespace DestinyLib.Operations
{
    using System.Collections.Generic;
    using System.Linq;

    using DestinyLib.DataContract;
    using DestinyLib.DataContract.Definitions;

    public static class PerkPermutationGenerator
    {
        public static List<PerkPermutation> GetPerkPermutations(WeaponPerkSetCollection weaponPerkSetCollection)
        {
            return GetPerkPermutations(weaponPerkSetCollection, Options.Default);
        }

        /// <summary>
        /// Given a <see cref="WeaponDefinition"/> generate every possible PerkPermutation.
        /// </summary>
        /// <param name="weaponPerkSetCollection"></param>
        /// <returns></returns>
        public static List<PerkPermutation> GetPerkPermutations(WeaponPerkSetCollection weaponPerkSetCollection, Options options)
        {
            // Use Breadth-First traversal to calculate all possible permutations.
            var perkSetList = weaponPerkSetCollection.Values;

            // BREADTH-FIRST
            List<PerkPermutation> permutations = new List<PerkPermutation>();

            // outer: PerkSets
            foreach (WeaponPerkSetDefinition perkSet in perkSetList)
            {
                // Ability Perks can affect stats, but this isn't useful for comparisons
                if (options.BehaviorExcludeAbilityPerks && perkSet.SocketTypeHash == 2614797986)
                {
                    continue;
                }

                var tempPermutations = permutations;
                permutations = new List<PerkPermutation>();

                //inner: Perk:
                foreach (WeaponPerkDefinition perk in perkSet.Values)
                {
                    //inner: Perk.Values (Note: not all perks have stat values)
                    if (options.BehaviorExcludePerksWithNoValue && perk.WeaponPerkValueList == null)
                    {
                        continue;
                    }

                    // combine with temp
                    if (tempPermutations.Any())
                    {
                        foreach (var temp in tempPermutations)
                        {
                            var newPermutation = temp.DeepClone();
                            newPermutation.WeaponPerkList.Add(perk);

                            permutations.Add(newPermutation);
                        }
                    }
                    else
                    {
                        permutations.Add(new PerkPermutation { WeaponPerkList = new List<WeaponPerkDefinition>() { perk } });
                    }
                }

                if (!permutations.Any())
                {
                    permutations = tempPermutations;
                }
            }

            return permutations;
        }

        public class Options
        {
            public static Options Default => new ()
            {
                BehaviorExcludePerksWithNoValue = true,
                BehaviorExcludeAbilityPerks = true,
            };

            public bool BehaviorExcludePerksWithNoValue { get; set; }

            public bool BehaviorExcludeAbilityPerks { get; set; }
        }
    }
}
