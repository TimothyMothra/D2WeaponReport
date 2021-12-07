namespace Tests.Operations
{
    using DestinyLib.Operations;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InterpolationCalculatorTests
    {
        [TestMethod]
        public void TestFunc()
        {
            var baseValue = 55;
            var perk = 17;
            var input = baseValue + perk;
            double x1 = 0, y1 = 10;
            double x2 = 100, y2 = 100;

            var result1 = InterpolationCalculator.Function(input: input, x1: x1, y1: y1, x2: x2, y2: y2, roundResult: false);
            Assert.AreEqual(expected: 74.8, actual: result1);

            var result2 = InterpolationCalculator.Function(input: input, x1: x1, y1: y1, x2: x2, y2: y2, roundResult: true);
            Assert.AreEqual(expected: 75, actual: result2);
        }
    }
}
