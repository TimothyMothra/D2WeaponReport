namespace DestinyLib.DataContract.Definitions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WeaponPerksCollection
    {
        public WeaponPerksCollection()
        {
            this.Values = new List<WeaponPerkSetDefinition>();
        }

        public IList<WeaponPerkSetDefinition> Values { get; set; }
    }
}
