using System.Collections.Generic;

namespace DestinyLib.Database.DataContract.Definitions
{
    public class WeaponPerkDefinition
    {
        public WeaponPerkMetaData MetaData { get; set; }

        public IList<WeaponPerkValueDefinition> WeaponPerkValueList { get; set; }

        public override string ToString()
        {
            if (this.WeaponPerkValueList == null)
            {
                return $"{this.MetaData}, Perks Empty";
            }
            else
            {
                return $"{this.MetaData}, Perks Count {this.WeaponPerkValueList.Count}";
            }
        }

        public IList<uint> GetAffectedStatHashIds()
        {
            if (this.WeaponPerkValueList == null)
            {
                return new List<uint>();
            }
            else
            {
                var list = new List<uint>();

                foreach (var wpvd in this.WeaponPerkValueList)
                {
                    list.Add(wpvd.StatHash);
                }

                return list;
            }
        }
    }
}
