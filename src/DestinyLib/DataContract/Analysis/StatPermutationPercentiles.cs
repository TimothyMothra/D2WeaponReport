namespace DestinyLib.DataContract.Analysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// An instance of <see cref="Percentiles"/> based on a set of Stat Permunations.
    /// </summary>
    public class StatPermutationPercentiles
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatPermutationPercentiles"/> class.
        /// Collection of Values will be padded with zeros to meet the Total Count.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hashId"></param>
        /// <param name="values"></param>
        /// <param name="totalCount"></param>
        public StatPermutationPercentiles(uint hashId, string name, double baseValue, List<double> values, int totalCount, IList<Tuple<double, double>> interpolationData)
        {
            this.HashId = hashId;
            this.Name = name;
            this.BaseValue = baseValue;
            this.Count = values.Count;
            this.InterpolationData = interpolationData;

            // Note: not all perk permutations will affect each stat.
            // Need to pad the stat Permutations here with 0's so the percentiles are correct.
            if (values.Count < totalCount)
            {
                values.AddRange(Enumerable.Repeat(0d, totalCount - values.Count));
            }

            this.Percentiles = new Percentiles(values);
        }

        public string Name { get; private set; }

        public double BaseValue { get; private set; }

        public uint HashId { get; private set; }

        public int Count { get; private set; }

        public Percentiles Percentiles { get; private set; }

        public IList<Tuple<double, double>> InterpolationData { get; set; }

        /// <summary>
        /// <code>[{ x: 0, y: 20}, { x: 10, y: 10}, { x: 20, y: 15}, { x: 30, y: 10}]</code>.
        /// </summary>
        /// <returns></returns>
        public string GetInterpolationDataAsString()
        {
            if (this.InterpolationData == null)
            {
                return "null";
            }

            var list = string.Join(",", this.InterpolationData.Select(x => $"{{x: {x.Item1}, y: {x.Item2}}}"));

            return $"[{list}]";
        }

        public override string ToString()
        {
            return $"{this.Name} ({this.Count}) ({this.Percentiles})";
        }
    }
}
