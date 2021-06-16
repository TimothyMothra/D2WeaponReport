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
        // TODO: FINISH THIS
    //    public PermutationsViewModel(WeaponDefinition weaponDefinition, WeaponSummary weaponSummary)
    //    {
    //        var perkNames = weaponSummary.Permutations.OrderByDescending(x => x.Value).Select(x => x.ToDisplayString()).ToList();

    //        this.Summary = new()
    //        {
    //            Name = weaponDefinition.MetaData.Name,
    //            BaseValue = weaponSummary.Base.ToString(),
    //            Values = weaponSummary.PermutationsAsString(),
    //            PermutationsCount = weaponSummary.Permutations.Count().ToString(),
    //            PerkNames = perkNames,
    //        };
    //    }


        // TODO: HOW DO I RETURN AN ARRAY?
        public string WeaponNamesForAutoComplete { get; set; }

        public SummaryDetails Summary { get; set; }

        public class SummaryDetails
        {
            public string Name { get; set; }
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
