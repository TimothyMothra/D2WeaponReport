namespace InitEnvironment
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"**ARG[{i}]: '{args[i]}'");
            }

            if (args.Length == 0)
            {
                Console.WriteLine("**ZERO ARGS RECEIVED");
                Console.WriteLine("This script will attempt to initialize the repo's environment directory, necessary for unit tests and debugging.");

                var rootDirectoryInfo = DestinyLib.LibEnvironment.GetRootDirectory();
                Console.WriteLine($"Root dir: '{rootDirectoryInfo.FullName}'");

                await DestinyLib.Scenarios.InitializeEnvironmentScenario.Run(rootDirectoryInfo: rootDirectoryInfo, deleteDirectory: false);
            }
            else if (args.Length == 1)
            {
                var dir = new DirectoryInfo(args[0]);
                await DestinyLib.Scenarios.InitializeEnvironmentScenario.Run(rootDirectoryInfo: dir, deleteDirectory: true);
            }
            else
            {
                throw new Exception($"Unexpected number of args: {args.Length}");
            }
        }
    }
}
