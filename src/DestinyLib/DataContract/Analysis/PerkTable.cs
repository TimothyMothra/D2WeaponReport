namespace DestinyLib.DataContract.Analysis
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using DestinyLib.Database.DataContract.Definitions;

    public class PerkTable
    {
        public PerkTable(WeaponDefinition weaponDefinition, IList<WeaponStatDefinition> stats, WeaponPerkSetDefinition perkSet)
        {
            this.WeaponDefinitionMetaData = weaponDefinition.MetaData;

            // Sanitize Input.
            // TODO: IF STAT IS NOT PRESENT IN PERK SET, EXCLUDE STAT FROM DISPLAY.
            var affectedStatIds = perkSet.Values.SelectMany(x => x.GetAffectedStatHashIds()).Distinct().ToList();

            this.Stats = stats.Where(x => affectedStatIds.Contains(x.MetaData.HashId)).ToList();

            this.PerkSet = perkSet;

            this.StatsToColumnIndex = new Dictionary<uint, int>();
            int columnIndex = 1;
            foreach (var stat in this.Stats)
            {
                this.StatsToColumnIndex.Add(stat.MetaData.HashId, columnIndex++);
            }
        }

        public IList<WeaponStatDefinition> Stats { get; set; }

        public WeaponPerkSetDefinition PerkSet { get; set; }

        private WeaponMetaData WeaponDefinitionMetaData { get; set; }

        private Dictionary<uint, int> StatsToColumnIndex { get; set; }

        public string GetDisplayName()
        {
            return $"SocketIndex: {this.PerkSet.SocketIndex} SocketTypeHash: {this.PerkSet.SocketTypeHash} PlugSetHash: {this.PerkSet.PlugSetHashId}";
        }

        public List<string> GetIconUris()
        {
            var iconUris = new List<string>(this.PerkSet.Values.Count);
            foreach (var perk in this.PerkSet.Values)
            {
                iconUris.Add(new Uri(LibEnvironment.GetDestinyHost(), perk.MetaData.IconPath).AbsoluteUri);
            }

            return iconUris;
        }

        public List<string> GetHeaderRow()
        {
            int numberOfColumns = this.Stats.Count + 1;

            // make header row. record column index of each stat
            var headerRow = new List<string>(numberOfColumns)
            {
                null, // top-left corner empty
            };

            foreach (var stat in this.Stats)
            {
                headerRow.Add(string.IsNullOrEmpty(stat.MetaData.Name) ? "-" : stat.MetaData.Name);
            }

            return headerRow;
        }

        public List<List<string>> GetDataDisplayTable()
        {
            int numberOfColumns = this.Stats.Count + 1;

            // enumerate perks and populate statContainer
            var rows = new List<List<string>>(this.PerkSet.Values.Count);
            foreach (var perk in this.PerkSet.Values)
            {
                var tempRow = new string[numberOfColumns];

                tempRow[0] = perk.MetaData.Name;

                if (perk.WeaponPerkValueList != null)
                {
                    foreach (var perkValue in perk.WeaponPerkValueList)
                    {
                        if (this.StatsToColumnIndex.TryGetValue(perkValue.StatHash, out int index))
                        {
                            tempRow[index] = perkValue.Value.ToString();
                        }
                        else
                        {
                            // TODO: SEVERAL WEAPONS HAVE PERKS THAT AFFECT STATS NOT FOUND IN THE DEFINITION. NEED TO QUERY DB FOR MISSING STATS.
                            Debug.WriteLine($"Missing Definition: Weapon: {this.WeaponDefinitionMetaData} Stat: {perkValue.StatHash}");
                        }
                    }
                }

                rows.Add(tempRow.ToList());
            }

            // export as rows of strings
            var exportTable = new List<List<string>>
            {
            };

            exportTable.AddRange(rows);

            return exportTable;
        }
    }
}
