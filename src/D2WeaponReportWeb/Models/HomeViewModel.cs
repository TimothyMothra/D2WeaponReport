namespace D2WeaponReportWeb.Models
{
    using System.Collections.Generic;

    using DestinyLib.Database.DataContract.Definitions;
    using DestinyLib.DataContract.Analysis;

    using static DestinyLib.Operations.WeaponPerkTableGenerator;

    public class HomeViewModel
    {
        public HomeViewModel(List<string> weaponNames)
        {
            this.WeaponNamesForAutoComplete = string.Join(",", weaponNames);

            // TODO: OFFLOAD LOGIC FROM CONTROLLER TO HERE.
        }

        // TODO: HOW DO I RETURN A JAVASCRIPT ARRAY? (For Boxplot function)
        public string WeaponNamesForAutoComplete { get; private set; }

        public WeaponDetailsViewModel WeaponDetails { get; set; }

        public string ErrorMessage { get; set; }

        public List<SearchResultViewModel> MultipleSearchResults { get; set; }

        public class WeaponDetailsViewModel
        {
            public List<PerkTableViewModel> PerkTables { get; set; }

            public List<PerkPermutationAnalysis> PerkPermutationAnalysisList { get; set; }

            public IList<StatPermutationPercentiles> StatPermutationPercentiles { get; internal set; }

            public WeaponDefinition WeaponDefinition { get; internal set; }
        }

        public class SearchResultViewModel
        {
            public string Name { get; set; }

            public uint Id { get; set; }

            public string IconUri { get; set; }
        }
    }
}
