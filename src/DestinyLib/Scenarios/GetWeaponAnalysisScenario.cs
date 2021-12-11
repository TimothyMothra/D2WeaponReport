namespace DestinyLib.Scenarios
{
    using System.Collections.Generic;
    using System.IO;

    using BungieLib.Manifest;
    using DestinyLib.Database;
    using DestinyLib.DataContract.Analysis;
    using DestinyLib.Operations;

    public static class GetWeaponAnalysisScenario
    {
        /// <summary>
        /// (https://github.com/mathnet/mathnet-numerics).
        /// (https://numerics.mathdotnet.com/DescriptiveStatistics.html).
        /// (https://plotly.com/javascript/box-plots/).
        /// (https://en.wikipedia.org/wiki/Box_plot).
        /// </summary>
        /// <param name="id"></param>
        public static List<PerkPermutationAnalysis> Run(uint id)
        {
            var dbPath = new FileInfo(LibEnvironment.GetDatabaseFilePath("world_sql_content"));
            var worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));
            var worldSqlContentProvider = new WorldSqlContentProvider(worldSqlContent, ProviderOptions.ScenarioDefault);

            // First get WeaponDefinition
            var weaponDefinition = worldSqlContentProvider.GetWeaponDefinition(id);

            // Then get analysis
            return WeaponAnalysisGenerator.GetPerkPermutationAnalysis(weaponDefinition);
        }
    }
}
