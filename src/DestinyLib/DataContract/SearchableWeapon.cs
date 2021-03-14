namespace DestinyLib.DataContract
{
    using System.Data;

    public class SearchableWeapon
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string ItemType { get; set; }

        public static SearchableWeapon Parse(IDataRecord record)
        {
            return new SearchableWeapon
            {
                Id = record.GetInt64(0),
                Name = record.GetString(1),
                ItemType = record.GetString(3),
            };
        }

        public override string ToString()
        {
            return $"{Id}_{Name}_{ItemType}";
        }
    }
}
