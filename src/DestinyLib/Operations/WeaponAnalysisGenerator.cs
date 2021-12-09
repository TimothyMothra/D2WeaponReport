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
            var statDefinitions = weaponDefinition.WeaponBaseStats.Values;
            var perkPermutations = PerkPermutationGenerator.GetPerkPermutations(weaponDefinition.WeaponPossiblePerks);

            if (!perkPermutations.Any())
            {
                // NOTE: SOME WEAPONS DO NOT CONTAIN PERKS. This will identify those during debug.
                // id:1619016919 name:Khvostov 7G-02
                // id:1744115122 name:Legend of Acrius
                Debug.WriteLine($"Missing Definition: Weapon: {weaponDefinition.MetaData}");

                return null;
            }

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
                    NetPoints = CalculateNetPoints(weaponDefinition, perkHashAndValues),
                    PercentileGrade = CalculatePercentileGrade(statPermutationPercentiles, perkPerm),
                    StatAndPercentileGrades = CalculateStatAndPercentileGrades(statPermutationPercentiles, perkPerm),
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

        private static List<Tuple<string, PercentileGrade>> CalculateStatAndPercentileGrades(List<StatPermutationPercentiles> statPermutationPercentiles, PerkPermutation perkPermutation)
        {
            var namesAndGrades = new List<Tuple<string, PercentileGrade>>();

            var statPermutation = new StatPermutation(perkPermutation);

            foreach (var statPerm in statPermutation.PerkHashAndValues)
            {
                var spp = statPermutationPercentiles.SingleOrDefault(x => x.HashId == statPerm.Key);
                if (spp != null)
                {
                    namesAndGrades.Add(new Tuple<string, PercentileGrade>(spp.Name, spp.Percentiles.GetPercentileGrade(statPerm.Value)));
                }
            }

            return namesAndGrades;
        }

        // TODO: SPLIT THIS INTO TWO METHODS
        private static string CalculatePercentileGrade(List<StatPermutationPercentiles> statPermutationPercentiles, PerkPermutation perkPermutation)
        {
            var grades = new List<PercentileGrade>();

            var statPermutation = new StatPermutation(perkPermutation);

            foreach (var statPerm in statPermutation.PerkHashAndValues)
            {
                var spp = statPermutationPercentiles.SingleOrDefault(x => x.HashId == statPerm.Key);
                if (spp != null)
                {
                    grades.Add(spp.Percentiles.GetPercentileGrade(statPerm.Value));
                }
            }

            var displayString = Percentiles.GetDisplayString(grades);
            return displayString;
        }

        private static double CalculateNetPoints(WeaponDefinition weaponDefinition, Dictionary<uint, double> perkHashAndValues)
        {
            // TODO: SOME STATS LESS IS PREFERRED (ex: charge time, draw time)

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
            var interpolationDefinitions = weaponDefinition.WeaponBaseStats.StatGroupDefinition.InterpolationDefinitions;

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
                var matchingInterpolationDefinition = interpolationDefinitions.SingleOrDefault(x => x.StatHashId == sd.Key);

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
                    totalCount: perkPermutations.Count,
                    interpolationData: matchingInterpolationDefinition?.DataPoints));
            }

            return statPermutationPercentiles;
        }
    }
}
