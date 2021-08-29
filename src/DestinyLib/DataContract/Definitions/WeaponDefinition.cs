namespace DestinyLib.DataContract.Definitions
{
    public class WeaponDefinition
    {
        public WeaponMetaData WeaponMetaData { get; set; }

        public WeaponStatsCollection WeaponBaseStats { get; set; }

        public WeaponPerksCollection WeaponPossiblePerks { get; set; }

        public override string ToString()
        {
            return $"[{this.WeaponMetaData.Id}] {this.WeaponMetaData.Name} (TODO: ItemType)"; // TODO: ITEM TYPE
        }
    }
}
