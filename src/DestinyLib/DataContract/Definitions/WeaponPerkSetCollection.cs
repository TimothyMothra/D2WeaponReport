namespace DestinyLib.DataContract.Definitions
{
    using System.Collections.Generic;

    public class WeaponPerkSetCollection
    {
        public WeaponPerkSetCollection()
        {
            this.Values = new List<WeaponPerkSetDefinition>();
        }

        public IList<WeaponPerkSetDefinition> Values { get; set; }
    }
}
