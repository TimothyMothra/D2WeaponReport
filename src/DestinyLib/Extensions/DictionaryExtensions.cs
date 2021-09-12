namespace DestinyLib.Extensions
{
    using System.Collections.Generic;

    public static class DictionaryExtensions
    {
        public static void CustomAdd(this Dictionary<uint, double> dictionary, uint key, double value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] += value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public static void CustomAdd(this Dictionary<uint, List<double>> dictionary, uint key, double value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key].Add(value);
            }
            else
            {
                dictionary.Add(key, new List<double> { value });
            }
        }
    }
}
