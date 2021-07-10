﻿namespace DestinyLib.Analysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using static DestinyLib.DataContract.WeaponDefinition;

    public class PerkTable
    {
        public PerkTable(IList<WeaponStat> stats, PerkSet perkSet)
        {
            this.Stats = stats;
            this.PerkSet = perkSet;
        }

        public IList<WeaponStat> Stats { get; set; }

        public PerkSet PerkSet { get; set; }

        public string GetDisplayName()
        {
            return $"SocketIndex: {this.PerkSet.SocketIndex} SocketTypeHash: {this.PerkSet.SocketTypeHash} PlugSetHash: {this.PerkSet.PlugSetHash}";
        }

        public List<string> GetIconUris()
        {
            List<string> iconUris = new List<string>(this.PerkSet.Perks.Count);
            foreach(var perk in this.PerkSet.Perks)
            {
                iconUris.Add(new Uri(LibEnvironment.GetDestinyHost(), perk.IconPath).AbsoluteUri);
            }
            return iconUris;
        }

        public List<string> GetHeaderRow()
        {
            int numberOfColumns = this.Stats.Count + 1;


            // make header row. record column index of each stat
            var headerRow = new List<string>(numberOfColumns);
            headerRow.Add(null); // top-left corner empty
            var statsToColumnIndex = new Dictionary<uint, int>();
            int columnIndex = 1;
            foreach (var stat in this.Stats)
            {
                headerRow.Add(string.IsNullOrEmpty(stat.Name) ? "-" : stat.Name);
                statsToColumnIndex.Add(stat.StatHash, columnIndex++);
            }

            return headerRow;
        }

        public List<List<string>> GetDataDisplayTable()
        {
            int numberOfColumns = this.Stats.Count + 1;

            var statsToColumnIndex = new Dictionary<uint, int>();
            int columnIndex = 1;
            foreach (var stat in this.Stats)
            {
                statsToColumnIndex.Add(stat.StatHash, columnIndex++);
            }

            // enumerate perks and populate statContainer
            List<List<string>> rows = new List<List<string>>(this.PerkSet.Perks.Count);
            foreach(var perk in this.PerkSet.Perks)
            {
                var tempRow = new string[numberOfColumns];

                tempRow[0] = perk.Name;

                if (perk.PerkValues != null)
                {
                    foreach (var perkValue in perk.PerkValues)
                    {
                        int index = statsToColumnIndex[perkValue.StatHash];
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
