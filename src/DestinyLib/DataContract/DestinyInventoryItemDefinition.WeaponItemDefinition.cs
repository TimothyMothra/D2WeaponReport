namespace DestinyLib.DataContract
{
    using Newtonsoft.Json;

    public class WeaponItemDefinition
    {
        public long DefaultDamageTypeHash { get; set; }

        public string Name { get; set; }
        

        public static WeaponItemDefinition Parse(string json)
        {
            dynamic record = JsonConvert.DeserializeObject(json);
            
            return new WeaponItemDefinition
            {
                DefaultDamageTypeHash = record.defaultDamageTypeHash,
                Name = record.displayProperties.name,
            };
        }
    }
}
