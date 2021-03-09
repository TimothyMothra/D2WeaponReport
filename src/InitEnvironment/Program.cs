namespace InitEnvironment
{
    using System;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] arg_s) => await DestinyLib.Interop.InitializeEnvironment.Run();
    }
}
