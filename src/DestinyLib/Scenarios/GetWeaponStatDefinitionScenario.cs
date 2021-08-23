namespace DestinyLib.Scenarios
{
    using System.IO;

    using DestinyLib.Database;
    using DestinyLib.DataContract;

    public static class GetWeaponStatDefinitionScenario
    {
        public static WeaponStatDefinition Run(uint id)
        {
            var dbPath = new FileInfo(LibEnvironment.GetDatabaseFilePath("world_sql_content"));
            var worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));
            var worldSqlContentProvider = new WorldSqlContentProvider(worldSqlContent, ProviderOptions.ScenarioDefault);
            var statDefinition = worldSqlContentProvider.GetWeaponStatDefinition(id);
            return statDefinition;
        }
    }
}
