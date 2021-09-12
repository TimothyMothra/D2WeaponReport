namespace DestinyLib.DataContract.Analysis
{
    using System.Collections.Generic;

    public class StatPermutationPercentiles
    {
        public StatPermutationPercentiles(string name, uint hashId, List<double> values)
        {
            this.Name = name;
            this.HashId = hashId;
            this.Count = values.Count;
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
