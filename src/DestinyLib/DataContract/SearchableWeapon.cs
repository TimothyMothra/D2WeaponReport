namespace DestinyLib.DataContract
{
    using System;
    using System.Data;

    public class SearchableWeaponRecord
    {
        public int Id { get; set; }
        public uint HashId { get; set; }
        public string Name { get; set; }

        public uint CollectibleHash { get; set; }

        public Uri IconUri { get; set; }

        public string ItemTypeDisplayName { get; set; }

        public static SearchableWeaponRecord Parse(IDataRecord record)
        {
            return new SearchableWeaponRecord
            {
                Id = record.GetInt32(0),
                HashId = Convert.ToUInt32(record.GetValue(1)),
                CollectibleHash = record.IsDBNull(2) ? default(uint) : Convert.ToUInt32(record.GetValue(2)),
                Name = record.GetString(3),
                ItemTypeDisplayName = record.GetString(4),
            };
        }

        public static SearchableWeaponRecord ParseWithIcons(IDataRecord record)
        {
            // the icon in the Collectible table includes the season watermark.
            var iconPath = record.IsDBNull(7) ? record.GetString(6) : record.GetString(7);

            return new SearchableWeaponRecord
            {
                Id = record.GetInt32(0),
                HashId = Convert.ToUInt32(record.GetValue(1)),
                CollectibleHash = record.IsDBNull(2) ? default(uint) : Convert.ToUInt32(record.GetValue(2)),
                Name = record.GetString(3),
                ItemTypeDisplayName = record.GetString(4),
                IconUri = new Uri(LibEnvironment.GetDestinyHost(), iconPath),
            };
        }

        public override string ToString()
        {
            return $"[{HashId}] {Name} ({ItemTypeDisplayName})";
        }
    }
}
