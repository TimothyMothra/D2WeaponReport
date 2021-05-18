namespace DestinyLib.Scenarios
{
    using DestinyLib.Database;
    using DestinyLib.DataContract;

    public static class GetWeaponDefinitionScenario
    {
        public static WeaponDefinition Run(uint id)
        {
            // TODO: THIS IS FINE FOR NOW. SCENARIOS CAN OWN THEIR OPTIONS.
            // IN THE FUTURE, TESTS WILL NEED TO SET THEIR OWN OPTIONS.
            // EX: THIS IS NEEDED TO TEST WITH AND WITHOUT CACHING.
            var providerOptions = new ProviderOptions
            {
                EnableCaching = false,
            };

            var dbPath = LibEnvironment.GetDatabaseFile("world_sql_content");
            var worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));
            
            var WorldSqlContentProvider = new WorldSqlContentProvider(worldSqlContent, providerOptions);
            var weaponDefinition = WorldSqlContentProvider.GetWeaponDefinition(id);
            return weaponDefinition;
        }
    }
}
