namespace DestinyLib
{
    using System;
    using System.IO;
    using System.Reflection;

    public static class Environment
    {
        private const string markerFileName = "root.marker"; // this file marks the root of the repo.
        private const string environmentDirectoryName = "environment";
        private static readonly string rootDirectory;
        private static readonly string environmentDirectory;


        static Environment()
        {
            rootDirectory = GetRootDirectory();
            environmentDirectory = GetEnvironmentDirectory();
        }

        private static string GetRootDirectory()
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            
            for(DirectoryInfo directory = new DirectoryInfo(assemblyLocation); directory != null ; directory = directory.Parent)
            {
                if (File.Exists(Path.Combine(directory.FullName, markerFileName)))
                {
                    return directory.FullName;
                }
            }

            return null;
        }

        public static string GetEnvironmentDirectory()
        {
            var environmentDirectory = Path.Combine(rootDirectory, environmentDirectoryName);

            if (Directory.Exists(environmentDirectory))
            {
                return environmentDirectory;
            }
            else
            {
                throw new Exception("Environment has not been initialized");
            }
        }

        public static string GetDatabaseFile(string fileNamePrefix)
        {
            DirectoryInfo di = new DirectoryInfo(environmentDirectory);
            FileInfo[] files = di.GetFiles($"{fileNamePrefix}*.content");

            if (files.Length == 1)
            {
                return files[0].FullName;
            }
            else if (files.Length == 0)
            {
                throw new Exception("Environment has not been initialized");
            }
            else
            {
                throw new Exception("Unknown error; more database files than expected were found. Please re-initialize the environment directory.");
            }
        }
    }
}
