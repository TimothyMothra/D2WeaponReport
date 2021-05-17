namespace DestinyLib.DataContract
{
    using System;
    using System.Collections.Generic;

    public class DestinyStatDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Interpolate { get; set; }
        public uint Id { get; set; }
    }
}
