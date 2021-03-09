namespace DestinyLib.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    using DestinyLib.Api;

    using Environment = DestinyLib.Environment;

    /// <summary>
    /// This class is to be called from the init-environment.ps1 PowerShell script.
    /// </summary>
    public class InitializeEnvironment
    {
        public static async Task Run()
        {
            var environmentDirectory = Environment.EnvironmentDirectory;

            var manifest = new Manifest();
            await manifest.DownloadWorldSqlContent(environmentDirectory);
        }

        public static string Test()
        {
            return "Hello World";
        }

    }
}
