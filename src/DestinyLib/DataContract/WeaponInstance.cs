namespace DestinyLib.DataContract
{
    using System.Collections.Generic;

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
