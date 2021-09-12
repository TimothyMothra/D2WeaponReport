namespace DestinyLib.DataContract.Analysis
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// An instance of <see cref="Percentiles"/> based on a set of Stat Permunations.
    /// </summary>
    public class StatPermutationPercentiles
    {
        public StatPermutationPercentiles(string name, uint hashId, List<double> values)
        {
            this.Name = name;
            this.HashId = hashId;
            this.Count = values.Count;
            this.Percentiles = new Percentiles(values);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatPermutationPercentiles"/> class.
        /// Collection of Values will be padded with zeros to meet the Total Count.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hashId"></param>
        /// <param name="values"></param>
        /// <param name="totalCount"></param>
        public StatPermutationPercentiles(string name, uint hashId, List<double> values, int totalCount)
        {
            this.Name = name;
            this.HashId = hashId;
            this.Count = values.Count;

            // Note: not all perk permutations will affect each stat.
            // Need to pad the stat Permutations here with 0's so the percentiles are correct.
            if (values.Count < totalCount)
            {
                values.AddRange(Enumerable.Repeat(0d, totalCount - values.Count));
            }

            this.Percentiles = new Percentiles(values);
        }

        public string Name { get; set; }

        public uint HashId { get; set; }

        public int Count { get; set; }

        public Percentiles Percentiles { get; set; }

        public override string ToString()
        {
            return $"{this.Name} ({this.Count}) ({this.Percentiles})";
        }
    }
}
