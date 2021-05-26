namespace DestinyLib.Scenarios
{
    using System.IO;

    using DestinyLib.Database;
    using DestinyLib.DataContract;

    public static class GetWeaponDefinitionScenario
    {
        public static WeaponDefinition Run(uint id)
        {
            var dbPath = new FileInfo(LibEnvironment.GetDatabaseFilePath("world_sql_content"));
            var worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));
            var WorldSqlContentProvider = new WorldSqlContentProvider(worldSqlContent, ProviderOptions.ScenarioDefault);
            var weaponDefinition = WorldSqlContentProvider.GetWeaponDefinition(id);
            return weaponDefinition;
        }
    }
}
