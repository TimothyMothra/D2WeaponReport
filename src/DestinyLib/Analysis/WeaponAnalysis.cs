namespace DestinyLib.Analysis
{
    using System.Collections.Generic;
    using System.Linq;

    using DestinyLib.DataContract;

    public static class WeaponAnalysis
    {
        private const bool BehaviorIncludePerksWithNoValue = false;
        private const bool BehaviorValidatePermutations = true;

        public static WeaponSummary GetWeaponSummary(WeaponDefinition weaponDefinition)
        {
            // Use Breadth-First traversal to calculate all possible permutations.
            var stats = weaponDefinition.Stats;

            var baseTotalPoints = 0;
            foreach(var stat in stats)
            {
                baseTotalPoints += stat.Value;
            }

            var perkSets = weaponDefinition.PerkSets;

            // BREADTH-FIRST
            List<PerkPermutation> permutations = null;

            // outer: PerkSets
            foreach(var perkSet in perkSets)
            {
                var tempPermutations = permutations ?? new List<PerkPermutation>();
                permutations = new List<PerkPermutation>();

                //inner: Perk: 
                foreach (var perk in perkSet.Perks)
                {
                    //inner: Perk.Values (Note: not all perks have values)
                    if (!BehaviorIncludePerksWithNoValue && perk.PerkValues == null)
                    {
                        continue;
                    }

                    // convert current perk to dictionary of key/values
                    var perkValuesAsDictionary = new Dictionary<uint, double>();
                    if (perk.PerkValues != null) 
                    {
                        foreach (var values in perk.PerkValues)
                        {
                            perkValuesAsDictionary.CustomAdd(values.StatHash, values.Value);
                        }
                    }

                    // combine with temp
                    if (tempPermutations.Any())
                    {
                        foreach (var temp in tempPermutations)
                        {
                            var newPermutation = new PerkPermutation
                            {
                                PerkNames = temp.PerkNames + $", {perk.Name}",
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
                        permutations.Add(new PerkPermutation { PerkNames = perk.Name, PerkHashAndValues = new Dictionary<uint, double>(perkValuesAsDictionary) });
                    }
                }

                if (!permutations.Any())
                {
                    permutations = tempPermutations;
                }
            }

            if (permutations == null)
            {
                // Two weapons do not have perks.
                // id:1619016919 name:Khvostov 7G-02
                // id:1744115122 name:Legend of Acrius
                return new WeaponSummary(baseTotalPoints, null);
            }

            if (BehaviorValidatePermutations)// && weaponDefinition.Stats.Any()) 
            {
                // Note that some expired weapons do not have perks
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
