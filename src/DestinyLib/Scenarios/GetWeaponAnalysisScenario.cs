namespace DestinyLib.Scenarios
{
    using System.IO;

    using DestinyLib.Analysis;
    using DestinyLib.Database;

    public static class GetWeaponAnalysisScenario
    {
        public static void Run(uint id)
        {
            var dbPath = new FileInfo(LibEnvironment.GetDatabaseFilePath("world_sql_content"));
            var worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));
            var WorldSqlContentProvider = new WorldSqlContentProvider(worldSqlContent, ProviderOptions.ScenarioDefault);

            // First get WeaponDefinition
            var weaponDefinition = WorldSqlContentProvider.GetWeaponDefinition(id);

            WeaponAnalysis.GetAllPossibleValues(weaponDefinition);
        }
    }
}
