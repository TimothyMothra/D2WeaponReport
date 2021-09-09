using System.Collections.Generic;

namespace DestinyLib.DataContract.Definitions
{
    public class WeaponPerkDefinition
    {
        public WeaponPerkMetaData MetaData { get; set; }

        public IList<WeaponPerkValueDefinition> WeaponPerkList { get; set; }

        public override string ToString()
        {
            if (this.WeaponPerkList == null)
            {
                return $"{this.MetaData}, Perks Empty";
            }
            else
            {
                return $"{this.MetaData}, Perks Count {this.WeaponPerkList.Count}";
            }
        }
    }
}
