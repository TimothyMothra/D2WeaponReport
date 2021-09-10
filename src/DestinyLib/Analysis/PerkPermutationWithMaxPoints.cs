using System;
using System.Collections.Generic;
using System.Linq;

using DestinyLib.DataContract;
using DestinyLib.DataContract.Definitions;

namespace DestinyLib.Analysis
{
    public class PerkPermutationWithMaxPoints
    {
        public PerkPermutationWithMaxPoints() { }

        public PerkPermutationWithMaxPoints(PerkPermutation perkPermutation)
        {
            foreach (var perk in perkPermutation.WeaponPerkList)
            {
                if (this.PerkNames == null)
                {
                    this.PerkNames = perk.MetaData.Name;
                }
                else
                {
                    this.PerkNames += $", {perk.MetaData.Name}";
                }

                foreach (var perkValue in perk.WeaponPerkValueList)
                {
                    if (this.PerkHashAndValues.ContainsKey(perkValue.StatHash))
                    {
                        this.PerkHashAndValues[perkValue.StatHash] += perkValue.Value;
                    }
                    else
                    {
                        this.PerkHashAndValues.Add(perkValue.StatHash, perkValue.Value);
                    }
                }
            }
        }

        public double MaxPoints { get; private set; }

        public string PerkNames { get; set; }

        public Dictionary<uint, double> PerkHashAndValues { get; set; } = new Dictionary<uint, double>();

        public string ToDisplayString() => $"{this.MaxPoints}: {this.PerkNames}";

        public void Validate(IList<WeaponStatDefinition> weaponStats)
        {
            double perkSum = 0;

            foreach (var perk in this.PerkHashAndValues)
            {
                WeaponStatDefinition stat = null;

                try
                {
                    stat = weaponStats.SingleOrDefault(x => x.MetaData.HashId == perk.Key);

                    if (stat == null)
                    {
                        // i dont't know what these are, but some perks don't have a matching stat on the weapon. It's possible that i'm not parsing something.
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"This perk was not found in the available stats. names '{this.PerkNames}', id '{perk.Key}', value '{perk.Value}'", ex);
                }

                perkSum += this.Validate(stat, perk.Value);
            }

            this.MaxPoints = perkSum;
        }

        private double Validate(WeaponStatDefinition stat, double perkValue)
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
    }
}
