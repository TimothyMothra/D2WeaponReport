using System.Collections.Generic;

namespace DestinyLib.DataContract.Definitions
{
    public class WeaponStatsCollection
    {
        public WeaponStatsCollection()
        {
            this.Values = new List<WeaponStatDefinition>();
        }

        public IList<WeaponStatDefinition> Values { get; set; }

    }
}