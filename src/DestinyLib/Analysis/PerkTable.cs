namespace DestinyLib.Analysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using static DestinyLib.DataContract.WeaponDefinition;

    public class PerkTable
    {
        public PerkTable(IList<WeaponStat> stats, PerkSet perkSet)
        {
            this.Stats = stats;
            this.PerkSet = perkSet;
        }

        public IList<WeaponStat> Stats { get; set; }

        public PerkSet PerkSet { get; set; }


    }
}
