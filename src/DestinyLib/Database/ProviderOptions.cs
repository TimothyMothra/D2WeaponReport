namespace DestinyLib.Database
{

    public class ProviderOptions
    {
        public static ProviderOptions ScenarioDefault
        {
            get => new ProviderOptions { EnableCaching = false, };
        }

        public static ProviderOptions TestWithCaching
        {
            get => new ProviderOptions { EnableCaching = true, };
        }

        public bool EnableCaching { get; set; }
    }
}
