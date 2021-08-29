namespace DestinyLib.DataContract.Definitions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WeaponStatDefinition
    {
        public uint StatHash { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Value { get; set; }

        public int MaxValue { get; set; }

        public int MinValue { get; set; }

        public int DisplayMaximum { get; set; }

        //public bool IsHidden { get; set; }

        public bool Interpolate { get; set; } // TODO: NEED AN EXAMPLE TO WRITE THIS ALGORITHM. See also: https://github.com/TimothyMothra/DestinySandbox/blob/main/journal/getting_started.md#how-to-query-all-available-perks-ex-rampage-outlaw

        public override string ToString()
        {
            return $"[{this.StatHash}] {this.Name}";
        }

        public bool IgnoreMaxValue()
        {
            // Some stats ignore their defined Max Value.
            // id '2961396640' name 'Charge Time'
            // id '4284893193' name 'Rounds Per Minute'
            // id '447667954' name 'Draw Time'
            // id '3871231066' name 'Magazine'

            return this.StatHash == 2961396640u || this.StatHash == 4284893193u || this.StatHash == 447667954u || this.StatHash == 3871231066u;
        }
    }
}
