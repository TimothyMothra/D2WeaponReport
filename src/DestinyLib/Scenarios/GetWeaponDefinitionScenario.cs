namespace DestinyLib.Scenarios
{
    using DestinyLib.Database;
    using DestinyLib.DataContract;

    public static class GetWeaponDefinitionScenario
    {
        public static WeaponDefinition Run(long id)
        {
            var dbPath = LibEnvironment.GetDatabaseFile("world_sql_content");
            var worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));
            var WorldSqlContentProvider = new WorldSqlContentProvider(worldSqlContent);
            var weapon = WorldSqlContentProvider.GetWeaponDefinition(id);
            return weapon;
        }
    }
}
