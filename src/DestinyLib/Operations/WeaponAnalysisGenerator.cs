namespace DestinyLib.Operations
{
    using System.Collections.Generic;
    using System.Linq;

    using DestinyLib.DataContract;
    using DestinyLib.DataContract.Analysis;
    using DestinyLib.DataContract.Definitions;

    public static class WeaponAnalysisGenerator
    {
        private const bool BehaviorIncludePerksWithNoValue = false;
        private const bool BehaviorValidatePermutations = true;

        public static WeaponAnalysisSummary GetWeaponAnalysisSummary(WeaponDefinition weaponDefinition)
        {
            var stats = weaponDefinition.WeaponBaseStats.Values;
            var perkSets = weaponDefinition.WeaponPossiblePerks.Values;

            var baseTotalPoints = GetBaseTotalPoints(weaponDefinition);

            List<PerkPermutation> permutations = PerkPermutationGenerator.GetPerkPermutations(weaponDefinition.WeaponPossiblePerks);
            List<MaxPointPermutations> permutationsWithMaxPoints = GetMaxPointPermutations(permutations);

            if (!permutationsWithMaxPoints.Any())
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
            return weaponDefinition.WeaponBaseStats.Values.Sum(x => x.Value);
        }

        private static List<MaxPointPermutations> GetMaxPointPermutations(IList<PerkPermutation> perkPermutations)
        {
            return perkPermutations.Select(x => new MaxPointPermutations(x)).ToList();
        }

        private static List<PerkTable> GetPerkTables(WeaponDefinition weaponDefinition)
        {
            var stats = weaponDefinition.WeaponBaseStats.Values;
            var perkSets = weaponDefinition.WeaponPossiblePerks.Values;

            return perkSets.Select(x => new PerkTable(stats, x)).ToList();
        }
    }
}
