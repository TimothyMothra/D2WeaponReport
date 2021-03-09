namespace DestinyLib.Interop
{
    using System.Threading.Tasks;

    public class Program
    {
        static async Task Main(string[] arg_s) => await InitializeEnvironment.Run();
    }
}
