namespace DestinyLib.DataContract
{
    using System.Collections.Generic;

    using DestinyLib.Extensions;

    public class StatPermutation
    {
        public StatPermutation(PerkPermutation perkPermutation)
        {
            foreach (var perk in perkPermutation.WeaponPerkList)
            {
                foreach (var perkValue in perk.WeaponPerkValueList)
                {
                    this.PerkHashAndValues.CustomAdd(perkValue.StatHash, perkValue.Value);
                }
            }
        }

        public Dictionary<uint, double> PerkHashAndValues { get; set; } = new Dictionary<uint, double>();
    }
}
