
namespace DestinyInteractiveSandbox
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using DestinyLib.Scenarios;

    using McMaster.Extensions.CommandLineUtils;

    /// <summary>
    /// (https://github.com/natemcmaster/CommandLineUtils).
    /// (https://github.com/natemcmaster/CommandLineUtils/blob/main/docs/samples/subcommands/builder-api/Program.cs).
    /// </summary>
    public static class Program
    {

        public static void Main(string[] args)
        {
            var app = GetApplicatiov();

            if (!args.Any())
            {
                // interactive mode
                app.Execute(args);

                while (true)
                {
                    try
                    {
                        Console.WriteLine();
                        var input = Prompt.GetString(">");
                        var inputArgs = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        app.Execute(inputArgs);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            else
            {
                app.Execute(args);
            }
        }

        private static CommandLineApplication GetApplicatiov()
        {
            var app = new CommandLineApplication()
            {
                Name = "Destiny Interactive Sandbox",
                Description = "An interactive console to execute commands against the DestinyLib.",
            };

            app.HelpOption(inherited: true);

            app.OnExecute(() =>
            {
                // this is executed if no commands are provided
                Console.WriteLine("No commands provided.");
                app.ShowHelp();
                return 1;
            });

            // this example is from one of the CommandLineUtils samples.
            app.Command("example", configCmd =>
            {
                configCmd.OnExecute(() =>
                {
                    Console.WriteLine("Specify a subcommand");
                    configCmd.ShowHelp();
                    return 1;
                });

                configCmd.Command("set", setCmd =>
                {
                    setCmd.Description = "Set config value";
                    var key = setCmd.Argument("key", "Name of the config").IsRequired();
                    var val = setCmd.Argument("value", "Value of the config").IsRequired();
                    setCmd.OnExecute(() =>
                    {
                        Console.WriteLine($"Setting config {key.Value} = {val.Value}");
                    });
                });

                configCmd.Command("list", listCmd =>
                {
                    var json = listCmd.Option("--json", "Json output", CommandOptionType.NoValue);
                    listCmd.OnExecute(() =>
                    {
                        if (json.HasValue())
                        {
                            Console.WriteLine("{\"dummy\": \"value\"}");
                        }
                        else
                        {
                            Console.WriteLine("dummy = value");
                        }
                    });
                });
            });

            app.Command("search", searchCmd =>
            {
                searchCmd.Description = "Search for a weapon.";
                var searchString = searchCmd.Argument("searchString", "Search string for a weapon. Uses wildcards by default.").IsRequired();
                var exact = searchCmd.Option("--exact", "Exact match.", CommandOptionType.NoValue);

                searchCmd.OnExecute(() =>
                {
                    var searchType = exact.HasValue() ? SearchForWeaponScenario.SearchType.StringContains : SearchForWeaponScenario.SearchType.Regex;
                    var weapons = SearchForWeaponScenario.Run(searchString.Value, searchType);
                    foreach (var w in weapons)
                    {
                        Console.WriteLine(w);
                    }
                });
            });

            app.Command("weapon", weaponCmd =>
            {
                weaponCmd.Description = "Get a weapon's details.";
                var weaponId = weaponCmd.Option<uint>("--id", "Id of a weapon", CommandOptionType.SingleValue).IsRequired();

                weaponCmd.OnExecute(() =>
                {
                    // TODO: WHAT IF ID DOES NOT EXIST?
                    var weapon = GetWeaponDefinitionScenario.Run(weaponId.ParsedValue);
                    Console.Write(weapon);
                });
            });

            return app;
        }
    }
}
