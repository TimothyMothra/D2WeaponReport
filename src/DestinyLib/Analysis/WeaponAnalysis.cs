namespace DestinyLib.Analysis
{
    using System.Collections.Generic;
    using System.Linq;

    using DestinyLib.DataContract;

    public static class WeaponAnalysis
    {
        private const bool BehaviorIncludePerksWithNoValue = false;
        private const bool BehaviorValidatePermutations = true;

        public static WeaponSummary GetAllPossibleValues(WeaponDefinition weaponDefinition)
        {
            // Use Breadth-First traversal to calculate all possible permutations.

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
                    //inner: Perk.Values (Note: not all perks have values)
                    if (!BehaviorIncludePerksWithNoValue && perk.PerkValues == null)
                    {
                        continue;
                    }


                    string perkName = perk.Name;
                    //double value = 0;


                    var perkValuesAsDictionary = new Dictionary<uint, double>();
                    if (perk.PerkValues != null) 
                    {
                        foreach (var values in perk.PerkValues)
                        {
                            perkValuesAsDictionary.CustomAdd(values.StatHash, values.Value);
                            //value += values.Value;
                        }
                    }

                    // combine with temp
                    if (tempPermutations.Any())
                    {

                        foreach (var temp in tempPermutations)
                        {
                            var newPermutation = new WeaponPermutation
                            {
                                PerkNames = temp.PerkNames + $", {perkName}",
                                PerkHashAndValues = new Dictionary<uint, double>(temp.PerkHashAndValues.AsEnumerable()), // TODO: THIS IS VERY WASTEFUL
                                //Value = temp.Value + value
                            };

                            foreach (var kvp in perkValuesAsDictionary)
                            {
                                newPermutation.PerkHashAndValues.CustomAdd(kvp.Key, kvp.Value);
                            }

                            permutations.Add(newPermutation);
                        }
                    }
                    else
                    {
                        permutations.Add(new WeaponPermutation { PerkNames = perkName });
                    }
                }

                if (!permutations.Any())
                {
                    permutations = tempPermutations;
                }
            }

            if (BehaviorValidatePermutations)
            {
                permutations.ForEach(x => x.Validate(weaponDefinition.Stats));
            }

            var summary = new WeaponSummary(baseTotalPoints, permutations);
            return summary;
        }

        private static void CustomAdd(this Dictionary<uint, double> dictionary, uint key, double value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] += value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }
    }
}
