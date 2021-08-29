namespace DestinyLib.DataContract
{
    /// <summary>
    /// Collectible items have an entry in a separate table with some unique metadata.
    /// </summary>
    public class DestinyCollectibleDefinition
    {
        public uint HashId { get; set; }

        public string Name { get; set; } // don't need name, but it will be helpful for debugging

        public uint ItemHash { get; set; }

        public string IconPath { get; set; }
    }
}
