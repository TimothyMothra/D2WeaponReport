namespace DestinyLib.Database
{

    public class ProviderOptions
    {
        public static ProviderOptions ScenarioDefault
        {
            get => new ProviderOptions { EnableCaching = false, };
        }

        public bool EnableCaching { get; set; }
    }
}
