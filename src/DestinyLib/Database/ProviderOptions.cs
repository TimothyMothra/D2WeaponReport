namespace DestinyLib.Database
{

    public class ProviderOptions
    {
        public static ProviderOptions ScenarioDefault => new() { EnableCaching = false, };
        
        public static ProviderOptions TestWithCaching => new() { EnableCaching = true, };

        public bool EnableCaching { get; set; }
    }
}
