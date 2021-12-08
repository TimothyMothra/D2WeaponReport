namespace DestinyLib.DataContract.Definitions
{
    using System;
    using System.Collections.Generic;

    public class WeaponStatGroupDefinition
    {
        public uint StatGroupHashId { get; set; }

        public IList<WeaponStatInterpolationDefinition> InterpolationDefinitions { get; set; }

        public class WeaponStatInterpolationDefinition
        {
            public uint StatHashId { get; set; }

            public int MaxValue { get; set; }

            public IList<Tuple<double, double>> DataPoints { get; set; }

#if DEBUG
            public override string ToString()
            {
                return $"{this.StatHashId}, DataPoints: {this.DataPoints?.Count ?? -1}";
            }
#endif
        }
    }
}
