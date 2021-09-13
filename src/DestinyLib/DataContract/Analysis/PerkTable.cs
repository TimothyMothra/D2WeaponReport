namespace DestinyLib.DataContract.Analysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using DestinyLib.DataContract.Definitions;

    public class PerkTable
    {
        public PerkTable(IList<WeaponStatDefinition> stats, WeaponPerkSetDefinition perkSet)
        {
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

        private Dictionary<uint, int> StatsToColumnIndex { get; set; }

        public string GetDisplayName()
        {
            return $"SocketIndex: {this.PerkSet.SocketIndex} SocketTypeHash: {this.PerkSet.SocketTypeHash} PlugSetHash: {this.PerkSet.PlugSetHashId}";
        }

        public List<string> GetIconUris()
        {
            List<string> iconUris = new List<string>(this.PerkSet.Values.Count);
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
            List<List<string>> rows = new List<List<string>>(this.PerkSet.Values.Count);
            foreach (var perk in this.PerkSet.Values)
            {
                var tempRow = new string[numberOfColumns];

                tempRow[0] = perk.MetaData.Name;

                if (perk.WeaponPerkValueList != null)
                {
                    foreach (var perkValue in perk.WeaponPerkValueList)
                    {
                        int index = this.StatsToColumnIndex[perkValue.StatHash];
                        tempRow[index] = perkValue.Value.ToString();
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
