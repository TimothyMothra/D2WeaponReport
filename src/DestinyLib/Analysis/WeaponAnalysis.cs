namespace DestinyLib.Analysis
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DestinyLib.DataContract;

    using MathNet.Numerics.Statistics;

    public static class WeaponAnalysis
    {
        public static WeaponSummary GetAllPossibleValues(WeaponDefinition weaponDefinition)
        {
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

            // extract perks into a 2d array
            var matrix = new List<List<double>>();

            //outer: PerkSets
            foreach (var perkSet in perkSets)
            {
                var perkSetValues = new List<double>();

                //inner: Perk: 
                foreach(var perk in perkSet.Perks)
                {
                    if (perk.PerkValues == null) // not all perks have values.
                    {
                        continue;
                    }

                    //inner: Perk.Values
                    double value = 0;
                    foreach (var values in perk.PerkValues)
                    {
                        value += values.Value;
                    }

                    perkSetValues.Add(value);
                }

                if (perkSetValues.Any()) // some perksets have unique abilities that do not modify base stats.
                {
                    matrix.Add(perkSetValues);
                }
            }


            // compute all possible permutations from the 2d matrix

            var permutations = new List<double>();

            // outer
            foreach(var column in matrix)
            {
                // initial case:
                if (!permutations.Any())
                {
                    permutations = column;
                    continue;
                }

                var previousPermutations = permutations;
                permutations = new List<double>();

                // inner
                foreach(var row in column)
                {
                    foreach(var value in previousPermutations)
                    {
                        permutations.Add(row + value);
                    }
                }
            }

            var summary = new WeaponSummary(baseTotalPoints, permutations);
            return summary;
        }
    }
}
