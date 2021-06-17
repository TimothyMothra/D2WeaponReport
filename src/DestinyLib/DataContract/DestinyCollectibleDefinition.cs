namespace DestinyLib.DataContract
{
    public class DestinyCollectibleDefinition
    {
        public uint HashId { get; set; }
        public string Name { get; set; } // don't need name, but it will be helpful for debugging
        public uint ItemHash { get; set; }
        public string IconPath { get; set; }
    }
}
