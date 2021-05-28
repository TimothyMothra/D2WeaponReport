namespace DestinyLib.Analysis
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Box_plot
    /// </summary>
    public class WeaponSummary
    {
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
}
