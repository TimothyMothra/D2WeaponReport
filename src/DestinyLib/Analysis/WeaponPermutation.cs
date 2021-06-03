namespace DestinyLib.Analysis
{
    public class WeaponPermutation
    {
        public double Value { get; set; }
        public string PerkNames { get; set; }

        public string ToDisplayString() => $"{Value}: {PerkNames}";
    }
}
