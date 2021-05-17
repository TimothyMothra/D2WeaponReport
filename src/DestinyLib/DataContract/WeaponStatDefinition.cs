namespace DestinyLib.DataContract
{
    using System;
    using System.Collections.Generic;

    public class WeaponStatDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Interpolate { get; set; }
        public uint Id { get; set; }
    }
}
