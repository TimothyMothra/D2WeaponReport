﻿namespace DestinyLib.Scenarios
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

        public static async Task Run(string[] arg)
        {
            if (arg.Length == 0)
            {
                Console.WriteLine("Zero args");

                var environmentDirectory = LibEnvironment.EnvironmentDirectory;

                var manifest = new Manifest();
                await manifest.DownloadWorldSqlContent(environmentDirectory);
            }
            else if (arg.Length == 1)
            {
                Console.WriteLine($"Arg[0]: '{arg[0]}'");

                void print(string x)
                {
                    Console.WriteLine($"{x}, Exists: {Directory.Exists(x)}");
                }

                print(Environment.CurrentDirectory);

                print(Path.Join(Environment.CurrentDirectory, arg[0]));

                // TODO: This value needs to come from LibEnvironment.
                await File.Create(Path.Join(Environment.CurrentDirectory, arg[0], "root.marker")).DisposeAsync();

                var manifest = new Manifest();
                await manifest.DownloadWorldSqlContent(arg[0]);
            }
            else
            {
                throw new Exception($"Unexpected number of args: {arg.Length}");
            }
        }

        public static string Test()
        {
            return "Hello World";
        }
    }
}
