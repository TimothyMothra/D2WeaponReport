namespace DestinyLib.DataContract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            public uint Id { get; set; }
            public string AmmoTypeId { get; set; }
            public string ItemTypeId { get; set; }
            public string TypeName { get; set; }
            public string FrameName { get; set; }
            public string FlavorText { get; set; }
            public string DefaultDamageTypeId { get; set; }
            public string DefaultDamageTypeHash { get; set; }
            
            /// <summary>
            /// Example: Legendary, Exotic.
            /// </summary>
            public string TierTypeName { get; set; }
            public Uri Icon { get; set; }
        }

        public class WeaponStat
        {
            public uint StatHash { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int Value { get; set; }
            public int MaxValue { get; set; }
            public int MinValue { get; set; }
            public int DisplayMaximum { get; set; }
            //public bool IsHidden { get; set; }
            public bool Interpolate { get; set; } // TODO: NEED AN EXAMPLE TO WRITE THIS ALGORITHM

            public override string ToString()
            {
                return $"[{StatHash}] {Name}";
            }
        }

        public class PerkSet
        {
            public int SocketIndex { get; set; }
            public uint SocketTypeHash { get; set; }
            public uint PlugSetHash { get; set; }
            public IList<Perk> Perks;

            public override string ToString()
            {
                if (Perks != null && Perks.Count > 0)
                {
                    return $"index {this.SocketIndex} plugsethash {this.PlugSetHash} perks: " + string.Join(",", Perks.Select(x => x.Name));
                }
                else
                {
                    return "";
                }
            }
        }

        public class Perk
        {
            // TODO: Will need Perk Icon.
            //public bool IsPerk { get; set; }
            //public bool IsMod { get; set; }
            //public bool IsMasterwork { get; set; }
            public uint Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public IList<PerkValue> PerkValues { get; set; }
        }

        public class PerkValue
        {
            public uint StatHash { get; set; }
            public int Value { get; set; }
        }
    }
}
