namespace DestinyLib.Database
{
    /// <summary>
    /// Options to control the behavior of Providers.
    /// </summary>
    public class ProviderOptions
    {
        public static ProviderOptions ScenarioDefault => new () { EnableCaching = true, };

        public static ProviderOptions TestDefault => new () { EnableCaching = false, };

        public static ProviderOptions TestWithCaching => new () { EnableCaching = true, };

        public bool EnableCaching { get; set; }
    }
}
