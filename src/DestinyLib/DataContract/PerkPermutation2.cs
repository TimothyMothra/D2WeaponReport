namespace DestinyLib.DataContract
{
    using System.Collections.Generic;

    using DestinyLib.DataContract.Definitions;

    public class PerkPermutation2
    {
        public IList<WeaponPerkDefinition> WeaponPerkList { get; set; }

        public PerkPermutation2 DeepClone()
        {
            var clone = new PerkPermutation2
            {
                WeaponPerkList = new List<WeaponPerkDefinition>(),
            };

            foreach (var perk in this.WeaponPerkList)
            {
                clone.WeaponPerkList.Add(perk);
            }

            return clone;
        }
    }
}
