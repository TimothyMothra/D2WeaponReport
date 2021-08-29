namespace DestinyLib.DataContract.Definitions
{
    using System.Collections.Generic;

    public class WeaponPerksCollection
    {
        public WeaponPerksCollection()
        {
            this.Values = new List<WeaponPerkSetDefinition>();
        }

        public IList<WeaponPerkSetDefinition> Values { get; set; }
    }
}
