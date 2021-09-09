using System.Collections.Generic;
using System.Linq;

namespace DestinyLib.DataContract.Definitions
{
    public class WeaponPerkSetDefinition
    {
        public int SocketIndex { get; set; }

        public uint SocketTypeHash { get; set; }

        public uint PlugSetHashId { get; set; }

        public IList<WeaponPerkDefinition> Values { get; set; }

        public override string ToString()
        {
            if (this.Values != null && this.Values.Count > 0)
            {
                return $"index {this.SocketIndex} plugsethash {this.PlugSetHashId} perks: " + string.Join(",", this.Values.Select(x => x.MetaData.Name));
            }
            else
            {
                return string.Empty;
            }
        }
    }
}