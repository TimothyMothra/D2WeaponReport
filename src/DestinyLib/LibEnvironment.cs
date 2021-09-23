﻿namespace DestinyLib
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// This class encapsulates the local file IO necessary to run this application.
    /// </summary>
    public static class LibEnvironment
    {
        public static readonly string EnvironmentDirectory;

        private const string MarkerFileName = "root.marker"; // this file marks the root of the repo.
        private const string EnvironmentDirectoryName = "environment";
        private static readonly string RootDirectory;
        private static readonly Uri DestinyHost = new Uri("https://bungie.net");

        static LibEnvironment()
        {
            RootDirectory = GetRootDirectory();
            EnvironmentDirectory = GetEnvironmentDirectory();
        }

        public static Uri GetDestinyHost() => DestinyHost;

        public static string GetEnvironmentDirectory(bool createIfDoesntExist = true)
        {
            var environmentDirectory = Path.Combine(RootDirectory, EnvironmentDirectoryName);

            // Create if directory doesn't exist.
            if (createIfDoesntExist && !Directory.Exists(environmentDirectory))
            {
                Directory.CreateDirectory(environmentDirectory);
            }

            // Eval before return.
            if (!Directory.Exists(environmentDirectory))
            {
                throw new Exception("Environment directory doesn't exist. Need to run the init-environment command.");
            }

            return environmentDirectory;
        }

        public static string GetDatabaseFilePath(string fileNamePrefix)
        {
            var di = new DirectoryInfo(EnvironmentDirectory);

            try
            {
                FileInfo[] files = di.GetFiles($"{fileNamePrefix}*.content");

                if (files.Length == 1)
                {
                    return files[0].FullName;
                }
                else if (files.Length == 0)
                {
                    Console.WriteLine($"DirectoryInfo: {di.FullName}");

                    throw new Exception("Environment has not been initialized");
                }
                else
                {
                    throw new Exception("Unknown error; more database files than expected were found. Please re-initialize the environment directory.");
                }
            }
            catch (Exception ex)
            {
                // Print directory contents
                var sb = new StringBuilder();

                sb.AppendLine($"**DIRECTORY EXCEPTION: {ex.Message}**");

                while (di != null)
                {
                    sb.AppendLine(di.FullName);
                    foreach (var f in di.GetFiles())
                    {
                        sb.AppendLine(f.FullName);
                    }

                    di = di.Parent;
                }

                throw new Exception(sb.ToString(), ex);
            }
        }

        private static string GetRootDirectory()
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;

            for (var directory = new DirectoryInfo(assemblyLocation); directory != null; directory = directory.Parent)
            {
                if (File.Exists(Path.Combine(directory.FullName, MarkerFileName)))
                {
                    return directory.FullName;
                }
            }

            throw new FileNotFoundException($"Could not find MarkerFile {MarkerFileName} in relative directory to assembly: {assemblyLocation}");
        }
    }
}
