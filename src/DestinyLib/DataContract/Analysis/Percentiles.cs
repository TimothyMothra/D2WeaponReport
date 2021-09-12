namespace DestinyLib.DataContract.Analysis
{
    using System.Collections.Generic;

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

        public override string ToString()
        {
            return $"({this.Minimum}, {this.FirstQuartile}, {this.Median}, {this.ThirdQuartile}, {this.Maximum})";
        }
    }
}
