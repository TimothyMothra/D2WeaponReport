namespace SandboxWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DestinyLib.DataContract;

    public class PermutationsViewModel
    {

        // TODO: HOW DO I RETURN AN ARRAY?
        public string WeaponNames { get; set; }

        public SummaryDetails Summary { get; set; }

        public class SummaryDetails
        {
            public string Name { get; set; }
            public string BaseValue { get; set; }
            public string Values { get; set; }
            public string PermutationsCount { get; set; }
        }
    }
}
