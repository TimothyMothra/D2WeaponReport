namespace InitEnvironment
{
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] arg_s) => await DestinyLib.Scenarios.InitializeEnvironmentScenario.Run();
    }
}
