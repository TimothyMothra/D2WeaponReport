namespace DestinyLib.Analysis
{
    using System.Collections.Generic;
    using System.Linq;

    using DestinyLib.DataContract;
    using DestinyLib.DataContract.Definitions;
    using DestinyLib.Operations;

    public static class WeaponAnalysis
    {
        private const bool BehaviorIncludePerksWithNoValue = false;
        private const bool BehaviorValidatePermutations = true;

        public static WeaponAnalysisSummary GetWeaponAnalysisSummary(WeaponDefinition weaponDefinition)
        {
            var stats = weaponDefinition.WeaponBaseStats.Values;
            var perkSets = weaponDefinition.WeaponPossiblePerks.Values;

            var baseTotalPoints = GetBaseTotalPoints(weaponDefinition);

            List<PerkPermutation> permutations = PerkPermutationGenerator.GetPerkPermutations(weaponDefinition.WeaponPossiblePerks);
            List<PerkPermutationWithMaxPoints> permutationsWithMaxPoints = GetPerkPermutations(permutations);

            if (permutationsWithMaxPoints == null)
            {
                // Two weapons do not have perks.
                // id:1619016919 name:Khvostov 7G-02
                // id:1744115122 name:Legend of Acrius
                return new WeaponAnalysisSummary();
            }

            if (BehaviorValidatePermutations) // && weaponDefinition.Stats.Any()) // TODO: WHAT WAS I DOING HERE?
            {
                // Note that some expired weapons do not have perks
                permutationsWithMaxPoints.ForEach(x => x.Validate(weaponDefinition.WeaponBaseStats.Values));
            }

            List<PerkTable> perkTables = GetPerkTables(weaponDefinition);

            var summary = new WeaponAnalysisSummary(baseTotalPoints, permutationsWithMaxPoints, perkTables);
            return summary;
        }

        private static int GetBaseTotalPoints(WeaponDefinition weaponDefinition)
        {
            var stats = weaponDefinition.WeaponBaseStats.Values;

            var baseTotalPoints = 0;
            foreach (var stat in stats)
            {
                baseTotalPoints += stat.Value;
            }

            return baseTotalPoints;
        }

        /// <summary>
        /// Given a list of <see cref="PerkPermutation"/> calculate the max points.
        /// </summary>
        /// <returns></returns>
        private static List<PerkPermutationWithMaxPoints> GetPerkPermutations(IList<PerkPermutation> perkPermutations)
        {
            return perkPermutations.Select(x => new PerkPermutationWithMaxPoints(x)).ToList();
        }

        private static List<PerkTable> GetPerkTables(WeaponDefinition weaponDefinition)
        {
            var stats = weaponDefinition.WeaponBaseStats.Values;
            var perkSets = weaponDefinition.WeaponPossiblePerks.Values;

            var perkTables = new List<PerkTable>(perkSets.Count);
            foreach (var perkSet in perkSets)
            {
                perkTables.Add(new PerkTable(stats, perkSet));
            }

            return perkTables;
        }
    }
}
