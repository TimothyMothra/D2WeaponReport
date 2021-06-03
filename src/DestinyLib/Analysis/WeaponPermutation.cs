using System.Collections.Generic;
using System.Linq;

using DestinyLib.DataContract;

namespace DestinyLib.Analysis
{
    public class WeaponPermutation
    {
        public List<WeaponDefinition.PerkValue> PerkValues { get; set; }

        public double PerkSum { get; set; }

        public string PerkNames { get; set; }

        public string ToDisplayString() => $"{PerkSum}: {PerkNames}";

        // TODO: THIS IS VERY WASTEFUL.
        public Dictionary<uint, double> PerkHashAndValues { get; set; } = new Dictionary<uint, double>();

        public void Validate(IList<WeaponDefinition.WeaponStat> weaponStats)
        {
            double perkSum = 0;

            foreach(var perk in PerkHashAndValues)
            {
                var stat = weaponStats.Single(x => x.StatHash == perk.Key);
                var statMax = stat.MaxValue != 0 ? stat.MaxValue : stat.DisplayMaximum;

                var tempSum = stat.Value + perk.Value;

                if(stat.MinValue <= tempSum && tempSum <= statMax)
                {
                    perkSum += perk.Value;
                }
            }

            this.PerkSum = perkSum;
        }
    }
}
