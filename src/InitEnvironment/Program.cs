namespace InitEnvironment
{
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main(string[] arg_s) => await DestinyLib.Scenarios.InitializeEnvironmentScenario.Run();
    }
}
