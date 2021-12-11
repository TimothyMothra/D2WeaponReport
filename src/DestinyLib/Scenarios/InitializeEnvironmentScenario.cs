namespace DestinyLib.Scenarios
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using BungieLib.Manifest;

    /// <summary>
    /// This scenario can make a root directory, create the root.marker, and download the manifest database.
    /// </summary>
    public class InitializeEnvironmentScenario
    {
        public static async Task Run(DirectoryInfo rootDirectoryInfo, bool deleteDirectory)
        {
            // TODO: NEED ILOGGER AS A PARAM TO OUTPUT STATUS (ex: command line)

            LibEnvironment.MakeRootDirectory(rootDirectoryInfo, deleteDirectory, out DirectoryInfo envDirectoryInfo);

            var manifest = new Manifest();
            await manifest.DownloadWorldSqlContent(envDirectoryInfo);
        }

        public static string Test()
        {
            return "Hello World";
        }
    }
}
