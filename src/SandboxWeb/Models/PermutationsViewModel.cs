namespace SandboxWeb.Models
{
    using DestinyLib.DataContract;

    using System.Collections.Generic;

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

        public class WeaponDetailsViewModel
        {
            public MetaDataViewModel MetaData { get; set; }
            public List<PerkTableViewModel> PerkTables { get; set; }
            public string BaseValue { get; set; }
            public string PermutationValues { get; set; }
            public string PermutationsCount { get; set; }
            public List<string> PerkNames { get; set; }

            public class PerkTableViewModel
            {
                public string TableDisplayName { get; set; }
                public List<List<string>> Rows { get; set; }
                // TODO: Need Perk Icons in here
            }

            public class MetaDataViewModel
            {
                public string Name { get; set; }
                public string IconUri { get; set; }
                public string ScreenshotUri { get; set; }
            }
        }

        public string ErrorMessage { get; set; }

        public List<SearchResultViewModel> MultipleSearchResults { get; set; }

        public class SearchResultViewModel
        {
            public string Name { get; set; }
            public uint Id { get; set; }
            public string IconUri { get; set; }
        }
    }
}
