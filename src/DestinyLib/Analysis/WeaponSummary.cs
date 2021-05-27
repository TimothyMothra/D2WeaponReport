namespace DestinyLib.Analysis
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Box_plot
    /// </summary>
    public class WeaponSummary
    {
        /// <summary>
        /// 0th percentile: the lowest data point.
        /// </summary>
        public decimal Minimum { get; set; }

        /// <summary>
        /// 25th percentile: the lower quartile.
        /// </summary>
        public decimal FirstQuartile { get; set; }

        /// <summary>
        /// 50th percentile: the middle value of the dataset.
        /// </summary>
        public decimal Median { get; set; }

        /// <summary>
        /// 75th percentile: the upper quartile.
        /// </summary>
        public decimal ThirdQuartile { get; set; }

        /// <summary>
        /// 100th percentile: the largest data point.
        /// </summary>
        public decimal Maximum { get; set; }
    }
}
