namespace DestinyLib.Scenarios
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using DestinyLib.Api;

    /// <summary>
    /// This class is to be called from the init-environment.ps1 PowerShell script.
    /// </summary>
    public class InitializeEnvironmentScenario
    {
        //public static async Task Run()
        //{
        //    Console.WriteLine("Empty Ctr");

        //    var environmentDirectory = LibEnvironment.EnvironmentDirectory;

        //    var manifest = new Manifest();
        //    await manifest.DownloadWorldSqlContent(environmentDirectory);
        //}

        public static async Task Run(DirectoryInfo rootDirectoryInfo, bool deleteDirectory)
        {
            // TODO: NEED ILOGGER AS A PARAM TO OUTPUT STATUS (ex: command line)

            LibEnvironment.MakeRootDirectory(rootDirectoryInfo, deleteDirectory, out DirectoryInfo envDirectoryInfo);

            var manifest = new Manifest();
            await manifest.DownloadWorldSqlContent(envDirectoryInfo);

            //if (arg.Length == 0)
            //{
            //    Console.WriteLine("Zero args");

            //    var environmentDirectory = LibEnvironment.EnvironmentDirectory;

            //    var manifest = new Manifest();
            //    await manifest.DownloadWorldSqlContent(environmentDirectory);
            //}
            //else if (arg.Length == 1)
            //{
            //    Console.WriteLine($"Arg[0]: '{arg[0]}'");

            //    var fi = arg[0].EndsWith("root.marker")
            //        ? new FileInfo(arg[0])
            //        : new FileInfo(Path.Combine(arg[0], "root.marker"));

            //    // SELF INITAILIZE: THIS IS DANGEROUS
            //    if (fi.Exists)
            //    {
            //        // TODO: This value needs to come from LibEnvironment.
            //        var di = new DirectoryInfo(Path.Combine(fi.Directory.FullName, "environment"));

            //        if (!di.Exists)
            //        {
            //            di.Create();
            //        }
            //        else
            //        {
            //            di.Delete(recursive: true);
            //            di.Create();
            //        }

            //    }
            //    else
            //    {
            //        throw new ArgumentException($"root.marker not found. '{arg[0]}'");
            //    }
            //}
            //else
            //{
            //    throw new Exception($"Unexpected number of args: {arg.Length}");
            //}
        }

        public static string Test()
        {
            return "Hello World";
        }
    }
}
