namespace DestinyLib.Scenarios
{
    using System.IO;

    using DestinyLib.Analysis;
    using DestinyLib.Database;

    public static class GetWeaponAnalysisScenario
    {
        /// <summary>
        /// (https://github.com/mathnet/mathnet-numerics).
        /// (https://numerics.mathdotnet.com/DescriptiveStatistics.html).
        /// (https://plotly.com/javascript/box-plots/).
        /// (https://en.wikipedia.org/wiki/Box_plot).
        /// </summary>
        /// <param name="id"></param>
        public static WeaponSummary Run(uint id)
        {
            var dbPath = new FileInfo(LibEnvironment.GetDatabaseFilePath("world_sql_content"));
            var worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));
            var WorldSqlContentProvider = new WorldSqlContentProvider(worldSqlContent, ProviderOptions.ScenarioDefault);

            // First get WeaponDefinition
            var weaponDefinition = WorldSqlContentProvider.GetWeaponDefinition(id);

            return WeaponAnalysis.GetAllPossibleValues(weaponDefinition);
        }
    }
}
