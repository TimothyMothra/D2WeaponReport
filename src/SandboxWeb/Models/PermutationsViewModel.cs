namespace SandboxWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DestinyLib.Analysis;
    using DestinyLib.DataContract;

    public class PermutationsViewModel
    {
        public PermutationsViewModel(List<string> weaponNames)
        {
            this.WeaponNamesForAutoComplete = string.Join(",", weaponNames);

            // TODO: OFFLOAD LOGIC FROM CONTROLLER TO HERE.
        }

        // TODO: HOW DO I RETURN AN ARRAY?
        public string WeaponNamesForAutoComplete { get; private set; }

        public SummaryDetails Summary { get; set; }

        public class SummaryDetails
        {
            public string Name { get; set; }
            public string IconUri { get; set; }
            public string BaseValue { get; set; }
            public string Values { get; set; }
            public string PermutationsCount { get; set; }
            public List<string> PerkNames { get; set; }
        }

        public string Error { get; set; }

        public List<SearchResult> MultipleResults { get; set; }

        public class SearchResult
        {
            public string Name { get; set; }
            public uint Id { get; set; }
            public string IconUri { get; set; }
        }
    }
}
