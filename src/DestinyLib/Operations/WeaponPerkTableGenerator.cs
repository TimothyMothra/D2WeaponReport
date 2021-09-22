namespace DestinyLib.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using DestinyLib.DataContract;
    using DestinyLib.DataContract.Analysis;
    using DestinyLib.DataContract.Definitions;
    using DestinyLib.Extensions;

    public static class WeaponPerkTableGenerator
    {
        public static List<PerkTableViewModel> GetPerkTableViewModel(WeaponDefinition weaponDefinition)
        {
            var perkTables = GetPerkTables(weaponDefinition);

            var perkTableViewModel = new List<PerkTableViewModel>(perkTables.Count);

            foreach (var analysisPerkTable in perkTables)
            {
                perkTableViewModel.Add(new PerkTableViewModel
                {
                    TableDisplayName = analysisPerkTable.GetDisplayName(),
                    HeaderRow = analysisPerkTable.GetHeaderRow(),
                    Rows = analysisPerkTable.GetDataDisplayTable(),
                    IconUris = analysisPerkTable.GetIconUris(),
                });
            }

            return perkTableViewModel;
        }

        private static List<PerkTable> GetPerkTables(WeaponDefinition weaponDefinition)
        {
            var stats = weaponDefinition.WeaponBaseStats.Values;
            var perkSets = weaponDefinition.WeaponPossiblePerks.Values;

            return perkSets.Select(x => new PerkTable(weaponDefinition, stats, x)).ToList();
        }

        public class PerkTableViewModel
        {
            public string TableDisplayName { get; set; }

            public List<string> HeaderRow { get; set; }

            public List<string> IconUris { get; set; }

            public List<List<string>> Rows { get; set; }
        }
    }
}
