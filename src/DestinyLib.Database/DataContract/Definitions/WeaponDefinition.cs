namespace DestinyLib.Database.DataContract.Definitions
{
    public class WeaponDefinition
    {
        public WeaponDefinition(WeaponMetaData weaponMetaData, WeaponStatsCollection weaponStatsCollection, WeaponPerkSetCollection weaponPerksCollection)
        {
            this.MetaData = weaponMetaData;
            this.WeaponBaseStats = weaponStatsCollection;
            this.WeaponPossiblePerks = weaponPerksCollection;
        }

        public WeaponMetaData MetaData { get; set; }

        public WeaponStatsCollection WeaponBaseStats { get; set; }

        public WeaponPerkSetCollection WeaponPossiblePerks { get; set; }

        public override string ToString()
        {
            return this.MetaData.ToString();
        }
    }
}
