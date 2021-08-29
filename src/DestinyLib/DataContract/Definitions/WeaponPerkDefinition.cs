using System.Collections.Generic;

namespace DestinyLib.DataContract.Definitions
{
    public class WeaponPerkDefinition
    {
        //public bool IsPerk { get; set; }
        //public bool IsMod { get; set; }
        //public bool IsMasterwork { get; set; }
        public uint Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IList<WeaponPerkValueDefinition> WeaponPerkList { get; set; }

        // TODO: Will need Perk Icon to display in Table
        public string IconPath { get; set; }
    }
}
