﻿namespace DestinyLib.DataContract.Definitions
{
    public class WeaponPerkMetaData
    {
        public uint HashId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string IconPath { get; set; } // TODO: Will need Perk Icon to display in Table

        public override string ToString()
        {
            return $"{this.HashId} {this.Name}";
        }
    }
}
