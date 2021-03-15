namespace DestinyLib.Scenarios
{
    using System.Threading.Tasks;

    using DestinyLib.Api;

    /// <summary>
    /// This class is to be called from the init-environment.ps1 PowerShell script.
    /// </summary>
    public class InitializeEnvironmentScenario
    {
        public static async Task Run()
        {
            var environmentDirectory = LibEnvironment.EnvironmentDirectory;

            var manifest = new Manifest();
            await manifest.DownloadWorldSqlContent(environmentDirectory);
        }

        public static string Test()
        {
            return "Hello World";
        }
    }
}
