namespace DestinyLib.Analysis
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DestinyLib.DataContract;

    using MathNet.Numerics.Statistics;

    public static class WeaponAnalysis
    {
        private const bool BehaviorIncludePerksWithNoValue = false;

        public static WeaponSummary GetAllPossibleValues(WeaponDefinition weaponDefinition)
        {
            // Use Breadth-First traversal to calcualte all possible permutations.

            /// TODO: P1: CALCULATE ALL POSSIBLE VALUES --** PICK UP HERE **
            /// Need a holder for the Weapon Stats
            /// Need to foreach all combinations of Perks and add to the Stats.
            /// 
            /// With all combinations, need to sort min-max.
            /// Then calculate the <see cref="WeaponSummary"/>.

            var stats = weaponDefinition.Stats;

            var baseTotalPoints = 0;
            foreach(var stat in stats)
            {
                baseTotalPoints += stat.Value;
            }

            var perkSets = weaponDefinition.PerkSets;


            // BREADTH-FIRST
            List<WeaponPermutation> permutations = null;

            // outer: PerkSets
            foreach(var perkSet in perkSets)
            {
                var tempPermutations = permutations ?? new List<WeaponPermutation>();
                permutations = new List<WeaponPermutation>();

                //inner: Perk: 
                foreach (var perk in perkSet.Perks)
                {
                    string perkName = perk.Name;
                    double value = 0;

                    //inner: Perk.Values (Note: not all perks have values)

                    if (!BehaviorIncludePerksWithNoValue && perk.PerkValues == null)
                    {
                        continue;
                    }
                    


                    if (perk.PerkValues != null) 
                    {
                        foreach (var values in perk.PerkValues)
                        {
                            value += values.Value;
                        }
                    }

                    // combine with temp
                    if (tempPermutations.Any())
                    {

                        foreach (var temp in tempPermutations)
                        {
                            permutations.Add(new WeaponPermutation
                            {
                                PerkNames = temp.PerkNames + $", {perkName}",
                                Value = temp.Value + value
                            });
                        }
                    }
                    else
                    {
                        permutations.Add(new WeaponPermutation { PerkNames = perkName, Value = value });
                    }
                }

                if (!permutations.Any())
                {
                    permutations = tempPermutations;
                }
            }

            var summary = new WeaponSummary(baseTotalPoints, permutations);
            return summary;
        }
    }
}
