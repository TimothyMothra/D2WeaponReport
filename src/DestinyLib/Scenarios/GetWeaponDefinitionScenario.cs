namespace DestinyLib.Scenarios
{
    using System.IO;

    using BungieLib.Manifest;

    using DestinyLib.Database;
    using DestinyLib.Database.DataContract.Definitions;

    // TODO: SCENARIOS WOULD BENEFIT FROM DEPENDENCY INJECTION
    public static class GetWeaponDefinitionScenario
    {
        public static WeaponDefinition Run(uint id)
        {
            var dbPath = new FileInfo(LibEnvironment.GetDatabaseFilePath("world_sql_content"));
            var worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));
            var worldSqlContentProvider = new WorldSqlContentProvider(worldSqlContent, ProviderOptions.ScenarioDefault);
            var weaponDefinition = worldSqlContentProvider.GetWeaponDefinition(id);
            return weaponDefinition;
        }
    }
}
