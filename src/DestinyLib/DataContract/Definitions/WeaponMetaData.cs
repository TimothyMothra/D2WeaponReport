using System;

namespace DestinyLib.DataContract.Definitions
{
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

        public Uri GetIconUri() => new Uri(LibEnvironment.GetDestinyHost(), this.CollectionDefintitionIconPath ?? this.ItemDefinitionIconPath);

        public Uri GetScreenshotUri() => new Uri(LibEnvironment.GetDestinyHost(), this.ScreenshotPath);
    }
}