namespace DestinyLib.DataContract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WeaponInstance : WeaponDefinition
    {
        /// <summary>
        /// Assuming this can also hold Mods and MasterworkPerk.
        /// Can this also hold Ornament and Shader? 
        /// </summary>
        public IList<Perk> InstancePerks { get; set; }

        public bool IsMasterworked { get; set; }

        /// <summary>
        /// Where is this item; character, vault, postmaster
        /// </summary>
        public string Location { get; set; }
    }
}
