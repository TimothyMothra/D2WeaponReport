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
            return $"[{this.MetaData.Id}] {this.MetaData.Name} (xxxItemType)"; // TODO: ITEM TYPE
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

            public uint CollectibleHash { get; set; }

            /// <summary>
            /// Example: Legendary, Exotic.
            /// </summary>
            public string TierTypeName { get; set; }

            public string ItemDefinitionIconPath { get; set; }

            public string ScreenshotPath { get; set; }

            public string CollectionDefintitionIconPath { get; set; }

            public Uri GetIconUri() => new Uri(LibEnvironment.GetDestinyHost(), this.CollectionDefintitionIconPath == null ? this.ItemDefinitionIconPath : this.CollectionDefintitionIconPath);

            public Uri GetScreenshotUri() => new Uri(LibEnvironment.GetDestinyHost(), this.ScreenshotPath);
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

        public class PerkSet
        {
            public int SocketIndex { get; set; }

            public uint SocketTypeHash { get; set; }

            public uint PlugSetHash { get; set; }

            public IList<Perk> Perks { get; set; }

            public override string ToString()
            {
                if (this.Perks != null && this.Perks.Count > 0)
                {
                    return $"index {this.SocketIndex} plugsethash {this.PlugSetHash} perks: " + string.Join(",", this.Perks.Select(x => x.Name));
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public class Perk
        {
            // TODO: Will need Perk Icon to display in Table
            //public bool IsPerk { get; set; }
            //public bool IsMod { get; set; }
            //public bool IsMasterwork { get; set; }
            public uint Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public IList<PerkValue> PerkValues { get; set; }

            public string IconPath { get; set; }
        }

        public class PerkValue
        {
            public uint StatHash { get; set; }

            public int Value { get; set; }
        }
    }
}
