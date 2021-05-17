namespace DestinyLib.Scenarios
{
    using DestinyLib.Database;
    using DestinyLib.DataContract;

    public static class GetWeaponStatDefinitionScenario
    {
        public static WeaponStatDefinition Run(uint id)
        {
            var dbPath = LibEnvironment.GetDatabaseFile("world_sql_content");
            var worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));
            var WorldSqlContentProvider = new WorldSqlContentProvider(worldSqlContent);
            var statDefinition = WorldSqlContentProvider.GetWeaponStatDefinition(id);
            return statDefinition;
        }
    }
}
