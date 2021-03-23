namespace DestinyLib.DataContract
{
    using System;
    using System.Data;

    public class SearchableWeaponRecord
    {
        public int Id { get; set; }
        public uint HashId { get; set; }
        public string Name { get; set; }

        public string ItemTypeDisplayName { get; set; }

        public static SearchableWeaponRecord Parse(IDataRecord record)
        {
            return new SearchableWeaponRecord
            {
                Id = record.GetInt32(0),
                HashId = Convert.ToUInt32(record.GetValue(1)),
                Name = record.GetString(2),
                ItemTypeDisplayName = record.GetString(4),
            };
        }

        public override string ToString()
        {
            return $"[{HashId}] {Name} ({ItemTypeDisplayName})";
        }
    }
}
