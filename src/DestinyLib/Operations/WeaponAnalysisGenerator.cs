namespace DestinyLib.Operations
{
    using System;
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

        public static List<PerkPermutationAnalysis> GetPerkPermutationAnalysis(WeaponDefinition weaponDefinition)
        {
            // TODO: rewrite this to detect this condition without needing hardcoded exceptions
            if (weaponDefinition.MetaData.HashId == 1619016919
                || weaponDefinition.MetaData.HashId == 1744115122)
            {
                // Two weapons do not have perks.
                // id:1619016919 name:Khvostov 7G-02
                // id:1744115122 name:Legend of Acrius
                return null;
            }

            var statDefinitions = weaponDefinition.WeaponBaseStats.Values;
            var perkPermutations = PerkPermutationGenerator.GetPerkPermutations(weaponDefinition.WeaponPossiblePerks);
            var statPermutations = perkPermutations.Select(x => x.GetStatPermutation()).ToList();
            var statPermutationPercentiles = CalculateStatPermutationPercentiles(weaponDefinition, perkPermutations, statPermutations);

            var perkPermutationAnalysisList = new List<PerkPermutationAnalysis>();

            foreach (var perkPerm in perkPermutations)
            {
                var perkHashAndValues = new Dictionary<uint, double>();
                foreach (var perkValue in perkPerm.WeaponPerkList.SelectMany(x => x.WeaponPerkValueList))
                {
                    perkHashAndValues.CustomAdd(perkValue.StatHash, perkValue.Value);
                }

                perkPermutationAnalysisList.Add(new PerkPermutationAnalysis(perkPerm)
                {
                    MaxPoints = CalculateMaxPoints(weaponDefinition, perkHashAndValues),
                    PercentileGrade = CalculatePercentileGrade(statPermutationPercentiles, perkPerm),
                });
            }

            return perkPermutationAnalysisList;
        }

        public static List<StatPermutationPercentiles> GetStatPermutationPercentiles(WeaponDefinition weaponDefinition)
        {
            var statDefinitions = weaponDefinition.WeaponBaseStats.Values;
            var perkPermutations = PerkPermutationGenerator.GetPerkPermutations(weaponDefinition.WeaponPossiblePerks);
            var statPermutations = perkPermutations.Select(x => x.GetStatPermutation()).ToList();

            return CalculateStatPermutationPercentiles(weaponDefinition, perkPermutations, statPermutations);
        }

        private static string CalculatePercentileGrade(List<StatPermutationPercentiles> statPermutationPercentiles, PerkPermutation perkPermutation)
        {
            return "TODO: PercentileGrade"; //TODO: PercentileGrade
        }

        private static double CalculateMaxPoints(WeaponDefinition weaponDefinition, Dictionary<uint, double> perkHashAndValues)
        {
            var weaponStats = weaponDefinition.WeaponBaseStats.Values;

            double perkSum = 0;

            foreach (var perk in perkHashAndValues)
            {
                var matchingStatDefinition = weaponStats.SingleOrDefault(x => x.MetaData.HashId == perk.Key);

                if (matchingStatDefinition == null)
                {
                    // TODO: SEVERAL WEAPONS HAVE PERKS THAT AFFECT STATS NOT FOUND IN THE DEFINITION. NEED TO QUERY DB FOR MISSING STATS.
                    Debug.WriteLine($"Missing Definition: Weapon: {weaponDefinition.MetaData} Stat: {perk.Key}");
                    continue;
                }

                perkSum += ValidateStatDelta(matchingStatDefinition, perk.Value);
            }

            return perkSum;
        }

        private static double ValidateStatDelta(WeaponStatDefinition stat, double perkValue)
        {
            if (stat.IgnoreMaxValue())
            {
                return perkValue;
            }

            // ASSUMPTION: It is assumed that MaxValue or DisplayMaximum are set, but not both.
            if (stat.MaxValue != 0 && stat.DisplayMaximum != 0)
            {
                throw new Exception($"This stat breaks an assumption. id '{stat.MetaData.HashId}' name '{stat.MetaData.Name}' maxValue '{stat.MaxValue}' DisplayMax '{stat.DisplayMaximum}'");
            }

            var statAssumedMax = stat.MaxValue != 0 ? stat.MaxValue : stat.DisplayMaximum;

            // ASSUMPTION: It is assumed that a stat value can never be negative.
            if (stat.Value < stat.MinValue)
            {
                throw new Exception($"This stat breaks an assumption. id '{stat.MetaData.HashId}' name '{stat.MetaData.Name}' value '{stat.Value}' minValue '{stat.MinValue}'");
            }

            // ASSUMPTION: It is assumed that a stat value will not exceed max.
            if (stat.Value > statAssumedMax)
            {
                throw new Exception($"This stat breaks an assumption. id '{stat.MetaData.HashId}' name '{stat.MetaData.Name}' value '{stat.Value}' maxValue '{statAssumedMax}'");
            }

            double perkValueToRecord = perkValue;

            // If sum is lower than MinValue
            while ((stat.Value + perkValueToRecord) < stat.MinValue)
            {
                perkValueToRecord++;
            }

            //if sum is larger than MaxValue
            while ((stat.Value + perkValueToRecord) > statAssumedMax)
            {
                perkValueToRecord--;
            }

            return perkValueToRecord;
        }

        private static List<StatPermutationPercentiles> CalculateStatPermutationPercentiles(WeaponDefinition weaponDefinition, List<PerkPermutation> perkPermutations, List<StatPermutation> statPermutations)
        {
            var statDefinitions = weaponDefinition.WeaponBaseStats.Values;

            // Convert StatPermutations into Dictionary of StatId w/ list of values.
            var statsDictionary = new Dictionary<uint, List<double>>();
            foreach (var sp in statPermutations)
            {
                foreach (var kvp in sp.PerkHashAndValues)
                {
                    statsDictionary.CustomAdd(kvp.Key, kvp.Value);
                }
            }

            // Calculate Percertiles given all possible Stat Values.
            var statPermutationPercentiles = new List<StatPermutationPercentiles>();
            foreach (var sd in statsDictionary)
            {
                var matchingStatDefinition = statDefinitions.SingleOrDefault(x => x.MetaData.HashId == sd.Key);

                if (matchingStatDefinition == null)
                {
                    // TODO: SEVERAL WEAPONS HAVE PERKS THAT AFFECT STATS NOT FOUND IN THE DEFINITION. NEED TO QUERY DB FOR MISSING STATS.
                    Debug.WriteLine($"Missing Definition: Weapon: {weaponDefinition.MetaData} Stat: {sd.Key}");
                    continue;
                }

                statPermutationPercentiles.Add(new StatPermutationPercentiles(
                    name: matchingStatDefinition.MetaData.Name,
                    baseValue: matchingStatDefinition.Value,
                    hashId: sd.Key,
                    values: sd.Value,
                    totalCount: perkPermutations.Count));
            }

            return statPermutationPercentiles;
        }
    }
}
