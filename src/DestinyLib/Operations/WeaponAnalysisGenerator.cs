﻿namespace DestinyLib.Operations
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using DestinyLib.DataContract;
    using DestinyLib.DataContract.Analysis;
    using DestinyLib.DataContract.Definitions;
    using DestinyLib.Extensions;

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

        public static List<StatPermutationPercentiles> GetStatPermutationPercentiles(WeaponDefinition weaponDefinition)
        {
            var perkPermutations = PerkPermutationGenerator.GetPerkPermutations(weaponDefinition.WeaponPossiblePerks);
            var statPermutations = perkPermutations.Select(x => new StatPermutation(x)).ToList();

            Debug.Assert(statPermutations != null, "StatPermutations failed.");

            var permutationCount = perkPermutations.Count;

            var statsDictionary = new Dictionary<uint, List<double>>();
            foreach (var sp in statPermutations)
            {
                foreach (var kvp in sp.PerkHashAndValues)
                {
                    statsDictionary.CustomAdd(kvp.Key, kvp.Value);
                }
            }

            statsDictionary.ToString();

            ///

            var statDefinitions = weaponDefinition.WeaponBaseStats.Values;
            var metaDataList = statDefinitions.Select(x => x.MetaData).ToList();

            ///

            var statPermutationPercentiles = new List<StatPermutationPercentiles>();

            foreach (var sd in statsDictionary)
            {
                statPermutationPercentiles.Add(new StatPermutationPercentiles(
                    name: metaDataList.Single(x => x.HashId == sd.Key).Name,
                    hashId: sd.Key,
                    values: sd.Value,
                    totalCount: permutationCount));
            }

            return statPermutationPercentiles;
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
