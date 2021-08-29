using System.Collections.Generic;

namespace DestinyLib.DataContract.Definitions
{
    public class WeaponPerkDefinition
    {
        //public bool IsPerk { get; set; }
        //public bool IsMod { get; set; }
        //public bool IsMasterwork { get; set; }

        public WeaponPerkMetaData MetaData { get; set; }

        public IList<WeaponPerkValueDefinition> WeaponPerkList { get; set; }
    }
}
