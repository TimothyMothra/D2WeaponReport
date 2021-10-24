namespace DestinyLib.Api
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Net.Http;
    using System.Threading.Tasks;

    using DestinyLib.Database;

    using Newtonsoft.Json;

    public class Manifest
    {
        private const string BungieHostName = "https://www.bungie.net";
        private const string ManifestUri = "https://www.bungie.net/platform/Destiny2/Manifest";
        private readonly HttpClient httpClient = new ();
        private readonly Uri bungieHostUri;

        public Manifest()
        {
            this.bungieHostUri = new Uri(BungieHostName);
        }

        public static void UnzipContent(string filePath, string outputDirectory)
        {
            ZipFile.ExtractToDirectory(filePath, outputDirectory);
        }

        public async Task<string> GetManifest()
        {
            // TODO: NEED TO HANDLE TIMEOUTS AND POSSIBLE EXCEPTIONS.
            // TODO: TEST BY FORCING A SERVER ERROR 500. (GOOGLE)
            return await this.httpClient.GetStringAsync(ManifestUri);
        }

        public async Task<Uri> GetWorldSqlContentUri()
        {
            var jsonResponse = await this.GetManifest();

            dynamic jsonDynamic = JsonConvert.DeserializeObject(jsonResponse);
            string path = jsonDynamic.Response.mobileWorldContentPaths.en;

            return new Uri(this.bungieHostUri, path);
        }

        public async Task DownloadWorldSqlContent(DirectoryInfo directoryInfo)
        {
            try
            {
                var directory = directoryInfo.FullName;
                var uri = await this.GetWorldSqlContentUri();

                var fullPath = uri.AbsoluteUri;
                var fileName = fullPath.Substring(fullPath.IndexOf("world_sql_content"));
                var downloadFilePath = Path.Combine(directory, fileName + ".zip");

                await this.DownloadFile(uri, downloadFilePath);

                UnzipContent(downloadFilePath, directory);

                var assumedFilePath = new FileInfo(Path.Combine(directory, fileName));
                Database.TestConnection(assumedFilePath);
            }
            catch (HttpRequestException ex)
            {
                // TODO: I'M INFREQUENTLY SEEING 500. I ASSUME THIS IS THROTTLING, BUT I NEED TO SEE THE ACTUAL RESPONSE MESSAGE.
                ex.ToString();
                throw;
            }
            catch (Exception)
            {
                // TODO: HAVEN'T DECIDED HOW TO HANDLE THIS SCENARIO YET
                throw;
            }
        }

        private async Task DownloadFile(Uri uri, string downloadPath)
        {
            HttpResponseMessage response = await this.httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await response.Content.CopyToAsync(fileStream);
            }
        }
    }
}
