using System;

namespace DestinyLib.Database.DataContract.Definitions
{
    public class WeaponMetaData
    {
        public string Name { get; set; }

        public uint HashId { get; set; }

        public string AmmoTypeId { get; set; }

        public string ItemTypeId { get; set; }

        public string TypeName { get; set; }

        public string FrameName { get; set; }

        public string FrameDescription { get; set; }

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

        public Uri GetIconUri(Uri host) => new Uri(host, this.CollectionDefintitionIconPath ?? this.ItemDefinitionIconPath);

        public Uri GetScreenshotUri(Uri host) => new Uri(host, this.ScreenshotPath);

        public override string ToString() => $"{this.HashId} {this.Name} {this.TypeName}";
    }
}