namespace DestinyLib.DataContract.Analysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PerkPermutationAnalysis
    {
        public PerkPermutationAnalysis(PerkPermutation perkPermutation)
        {
            this.PerkPermutation = perkPermutation ?? throw new ArgumentNullException(nameof(perkPermutation));
        }

        public PerkPermutation PerkPermutation { get; private set; }

        public double MaxPoints { get; set; }

        public string PerkNames => string.Join(", ", this.PerkPermutation.WeaponPerkList.Select(x => x.MetaData.Name));

        public string PercentileGrade { get; set; }

        public List<Tuple<string, PercentileGrade>> StatAndPercentileGrades { get; internal set; }

        public string ToDisplayString() => $"{this.MaxPoints}: {this.PerkNames} ({this.PercentileGrade})";
    }
}
