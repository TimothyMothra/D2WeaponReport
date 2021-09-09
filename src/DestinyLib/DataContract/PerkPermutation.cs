namespace DestinyLib.DataContract
{
    using System.Collections.Generic;

    using DestinyLib.DataContract.Definitions;

    public class PerkPermutation
    {
        public IList<WeaponPerkDefinition> WeaponPerkList { get; set; }

        public PerkPermutation DeepClone()
        {
            var clone = new PerkPermutation
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
