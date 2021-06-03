using System.Collections.Generic;
using System.Linq;

using MathNet.Numerics.Statistics;

namespace DestinyLib.Analysis
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Box_plot
    /// </summary>
    public class WeaponSummary
    {

        public WeaponSummary(double baseValue, List<WeaponPermutation> permutations)
        {
            //TODO: UNIT TEST THIS AND CONFIRM IF SORT IS NEEDED

            var values = permutations.Select(x => x.Value).ToList();
            values.Sort();
            var permutationsEnumerable = values.AsEnumerable();

            //permutations.Sort();
            //var permutationsEnumerable = permutations.AsEnumerable();

            this.Base = baseValue;
            this.Minimum = Statistics.Percentile(permutationsEnumerable, 0);
            this.FirstQuartile = Statistics.Percentile(permutationsEnumerable, 25);
            this.Median = Statistics.Percentile(permutationsEnumerable, 50);
            this.ThirdQuartile = Statistics.Percentile(permutationsEnumerable, 75);
            this.Maximum = Statistics.Percentile(permutationsEnumerable, 100);
            this.Permutations = permutations;
        }

        /// <summary>
        /// This is the base value of the weapon stats, without perks.
        /// </summary>
        public double Base { get; set; }

        /// <summary>
        /// 0th percentile: the lowest data point.
        /// </summary>
        public double Minimum { get; set; }

        /// <summary>
        /// 25th percentile: the lower quartile.
        /// </summary>
        public double FirstQuartile { get; set; }

        /// <summary>
        /// 50th percentile: the middle value of the dataset.
        /// </summary>
        public double Median { get; set; }

        /// <summary>
        /// 75th percentile: the upper quartile.
        /// </summary>
        public double ThirdQuartile { get; set; }

        /// <summary>
        /// 100th percentile: the largest data point.
        /// </summary>
        public double Maximum { get; set; }

        public IList<WeaponPermutation> Permutations { get; set; }

        public string PermutationsAsString() => string.Join(",", Permutations.Select(x => x.Value).AsEnumerable());
    }
}
