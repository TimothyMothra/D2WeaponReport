namespace DestinyLib.Database
{

    public class ProviderOptions
    {
        public static ProviderOptions ScenarioDefault
        {
            get => new ProviderOptions { EnableCaching = false, };
        }

        // TODO: WILL NEED TO MOCK THE WORLDSQLCONTENT TO UNIT TEST THIS.
        public bool EnableCaching { get; set; }
    }
}
