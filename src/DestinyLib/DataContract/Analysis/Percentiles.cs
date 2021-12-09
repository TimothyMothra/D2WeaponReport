﻿namespace DestinyLib.DataContract.Analysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StatisticsApi = MathNet.Numerics.Statistics.Statistics;

    public class Percentiles
    {
        public Percentiles(IEnumerable<double> permutationsEnumerable)
        {
            this.Minimum = StatisticsApi.Percentile(permutationsEnumerable, 0);
            this.FirstQuartile = StatisticsApi.Percentile(permutationsEnumerable, 25);
            this.Median = StatisticsApi.Percentile(permutationsEnumerable, 50);
            this.ThirdQuartile = StatisticsApi.Percentile(permutationsEnumerable, 75);
            this.Maximum = StatisticsApi.Percentile(permutationsEnumerable, 100);
        }

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

        public static string GetDisplayString(List<PercentileGrade> percentileGrades)
        {
            return string.Join(string.Empty, percentileGrades
                    .GroupBy(x => x)
                    .OrderBy(x => x.Key)
                    .Select(x => $"{x.Key}{x.Count()}")
                    .AsEnumerable());
        }

        public override string ToString()
        {
            var minimum = Format(this.Minimum);
            var first = Format(this.FirstQuartile);
            var median = Format(this.Median);
            var third = Format(this.ThirdQuartile);
            var max = Format(this.Maximum);
            return $"({minimum}, {first}, {median}, {third}, {max})";
        }

        public PercentileGrade GetPercentileGrade(double value)
        {
            if (value > this.Maximum)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Exceeds Maximum");
            }
            else if (value > this.ThirdQuartile)
            {
                return PercentileGrade.A;
            }
            else if (value > this.Median)
            {
                return PercentileGrade.B;
            }
            else if (value > this.FirstQuartile)
            {
                return PercentileGrade.C;
            }
            else if (value >= this.Minimum)
            {
                return PercentileGrade.D;
            }
            else //if (value < this.Minimum)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Unexpected value");
            }
        }

        private static string Format(double x) => x > 0 ? $"+{x}" : x.ToString();
    }
}
