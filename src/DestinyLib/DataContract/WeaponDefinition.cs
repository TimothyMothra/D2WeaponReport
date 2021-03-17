﻿namespace DestinyLib.DataContract
{
    using System;
    using System.Collections.Generic;

    public class WeaponDefinition
    {
        public WeaponMetaData MetaData { get; set; }
        public IList<WeaponStat> Stats { get; set; }
        public IList<PerkSet> PerkSets { get; set; }

        public override string ToString()
        {
            return $"[{this.MetaData.Id}] {this.MetaData.Name} (xxxItemType)";
        }

        public class WeaponMetaData
        {
            public string Name { get; set; }
            public long Id { get; set; }
            public string AmmoTypeName { get; set; }
            public string TypeName { get; set; }
            public string FrameName { get; set; }
            public string FlavorText { get; set; }
            public string DefaultDamageTypeHash { get; set; }
            
            /// <summary>
            /// Example: Legendary, Exotic.
            /// </summary>
            public string TierTypeName { get; set; }
            public Uri Icon { get; set; }
        }

        public class WeaponStat
        {
            public string Name { get; set; }
            public int Value { get; set; }
            public int MaxValue { get; set; }
            public bool IsHidden { get; set; }
        }

        public class PerkSet
        {
            public int Index { get; set; }
            public IList<Perk> Perks;
        }

        public class Perk
        {
            public bool IsPerk { get; set; }
            public bool IsMod { get; set; }
            public bool IsMasterwork { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
            public int Value { get; set; }
        }
    }
}
