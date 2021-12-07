namespace DestinyLib.Operations
{
    using System;

    public static class InterpolationCalculator
    {
        /// <summary>
        /// <code> y =  (y2 – y1) / (x2 – x1) * (x – x1) + y1 </code>.
        /// </summary>
        public static double Function(double input, double x1, double x2, double y1, double y2, bool roundResult = false)
        {
            var result = (((y2 - y1) / (x2 - x1)) * (input - x1)) + y1;

            return roundResult ? Math.Round(result) : result;
        }
    }
}
