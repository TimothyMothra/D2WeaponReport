using System;
using System.Collections.Generic;
using System.Linq;

using DestinyLib.DataContract;

namespace DestinyLib.Analysis
{
    public class PerkPermutation
    {
        //public List<WeaponDefinition.PerkValue> PerkValues { get; set; }

        public double MaxPoints { get; private set; }

        public string PerkNames { get; set; }

        // TODO: THIS IS VERY WASTEFUL.
        public Dictionary<uint, double> PerkHashAndValues { get; set; } = new Dictionary<uint, double>();

        public string ToDisplayString() => $"{this.MaxPoints}: {this.PerkNames}";

        public void Validate(IList<WeaponDefinition.WeaponStat> weaponStats)
        {
            double perkSum = 0;

            foreach (var perk in this.PerkHashAndValues)
            {
                WeaponDefinition.WeaponStat stat = null;

                try
                {
                    stat = weaponStats.SingleOrDefault(x => x.StatHash == perk.Key);

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

        private double Validate(WeaponDefinition.WeaponStat stat, double perkValue)
        {
            if (stat.IgnoreMaxValue())
            {
                return perkValue;
            }

            // ASSUMPTION: It is assumed that MaxValue or DisplayMaximum are set, but not both.
            if (stat.MaxValue != 0 && stat.DisplayMaximum != 0)
            {
                throw new Exception($"This stat breaks an assumption. id '{stat.StatHash}' name '{stat.Name}' maxValue '{stat.MaxValue}' DisplayMax '{stat.DisplayMaximum}'");
            }

            var statAssumedMax = stat.MaxValue != 0 ? stat.MaxValue : stat.DisplayMaximum;

            // ASSUMPTION: It is assumed that a stat value can never be negative.
            if (stat.Value < stat.MinValue)
            {
                throw new Exception($"This stat breaks an assumption. id '{stat.StatHash}' name '{stat.Name}' value '{stat.Value}' minValue '{stat.MinValue}'");
            }

            // ASSUMPTION: It is assumed that a stat value will not exceed max.
            if (stat.Value > statAssumedMax)
            {
                throw new Exception($"This stat breaks an assumption. id '{stat.StatHash}' name '{stat.Name}' value '{stat.Value}' maxValue '{statAssumedMax}'");
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
