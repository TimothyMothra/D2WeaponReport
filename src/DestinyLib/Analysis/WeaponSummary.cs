﻿using System.Collections.Generic;
using System.Linq;

using StatisticsApi = MathNet.Numerics.Statistics.Statistics;

namespace DestinyLib.Analysis
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Box_plot
    /// </summary>
    public class WeaponSummary
    {

        public WeaponSummary(double baseValue, List<PerkPermutation> permutations)
        {
            if (permutations == null)
            {
                this.HasEmptyPerks = true;
                return;
            }

            this.Permutations = permutations;

            var values = permutations.Select(x => x.MaxPoints).ToList();
            values.Sort();
            var permutationsEnumerable = values.AsEnumerable();

            this.Statistics = new PermutationStatistics(baseValue, permutationsEnumerable);
        }

        public PermutationStatistics Statistics { get; set; }

        public class PermutationStatistics
        {
            public PermutationStatistics(double baseValue, IEnumerable<double> permutationsEnumerable)
            {
                this.Base = baseValue;
                this.Minimum = StatisticsApi.Percentile(permutationsEnumerable, 0);
                this.FirstQuartile = StatisticsApi.Percentile(permutationsEnumerable, 25);
                this.Median = StatisticsApi.Percentile(permutationsEnumerable, 50);
                this.ThirdQuartile = StatisticsApi.Percentile(permutationsEnumerable, 75);
                this.Maximum = StatisticsApi.Percentile(permutationsEnumerable, 100);
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

        }


        public IList<PerkTable> PerkTables { get; set; }


        public bool HasEmptyPerks { get; set; }

        public IList<PerkPermutation> Permutations { get; set; }

        public string PermutationsAsString() => string.Join(",", Permutations.Select(x => x.MaxPoints).AsEnumerable());


        public class PerkTable
        {
            // TODO: THIS
        }

    }
}
