namespace SandboxWeb.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using DestinyLib.Database;
    using DestinyLib.DataContract;
    using DestinyLib.Scenarios;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using SandboxWeb.Models;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        private readonly WorldSqlContentProvider worldSqlContentProvider;

        public HomeController(ILogger<HomeController> logger, WorldSqlContentProvider worldSqlContentProvider)
        {
            this.logger = logger;
            this.worldSqlContentProvider = worldSqlContentProvider ?? throw new ArgumentNullException(nameof(worldSqlContentProvider));
        }

        public IActionResult Index(string id)
        {
            var weapons = this.worldSqlContentProvider.GetSearchableWeapons();
            var weaponNames = weapons.Select(x => x.Name).ToList();

            var model = new HomeViewModel(weaponNames);

            if (id == null)
            {
                model.WeaponDetails = null;
            }
            else if (UInt32.TryParse(id, out uint hash))
            {
                model.WeaponDetails = GetWeaponDetails(hash);
            }
            else
            {
                var searchResults = SearchForWeaponScenario.Run(id, SearchForWeaponScenario.SearchType.Regex);

                if (searchResults.Count == 1)
                {
                    model.WeaponDetails = GetWeaponDetails(searchResults[0].HashId);
                }
                else if (searchResults.Count > 1 )
                {
                    model.MultipleSearchResults = GetMultipleResults(searchResults);
                }
                else
                {
                    model.ErrorMessage = "No Results Found";
                }
            }

            return View(model);
        }

        /// <summary>
        /// Given a list of multple search results, return a list of the data needed to display.
        /// </summary>
        /// <param name="searchableWeaponRecords"></param>
        /// <returns></returns>
        private static List<HomeViewModel.SearchResultViewModel> GetMultipleResults(IList<SearchableWeaponRecord> searchableWeaponRecords)
        {
            var displayResults = new List<HomeViewModel.SearchResultViewModel>(searchableWeaponRecords.Count);

            foreach (var searchableWeaponRecord in searchableWeaponRecords)
            {
                displayResults.Add(new HomeViewModel.SearchResultViewModel
                {
                    Name = searchableWeaponRecord.Name,
                    Id = searchableWeaponRecord.HashId,
                    IconUri = searchableWeaponRecord.GetIconUri().AbsoluteUri,
                });
            }

            return displayResults;
        }

        private static HomeViewModel.WeaponDetailsViewModel GetWeaponDetails(uint hash)
        {
            var definition = GetWeaponDefinitionScenario.Run(hash);
            var weaponSummary = GetWeaponAnalysisScenario.Run(hash);

            var perkNames = weaponSummary.Permutations.OrderByDescending(x => x.MaxPoints).Select(x => x.ToDisplayString()).ToList();

            return new()
            {
                MetaData = new()
                {
                    Name = definition.MetaData.Name,
                    IconUri = definition.MetaData.GetIconUri().AbsoluteUri,
                    ScreenshotUri = definition.MetaData.GetScreenshotUri().AbsoluteUri,
                },

                PerkTables = new()
                {
                    new()
                    {
                        TableDisplayName = "Test Perks",
                        Rows = new()
                        {
                            new() { null, "One", "Two", "Three" },
                            new() { "AAA", null, "+5", null },
                        }
                    }
                },

                BaseValue = weaponSummary.Base.ToString(),
                PermutationValues = weaponSummary.PermutationsAsString(),
                PermutationsCount = weaponSummary.Permutations.Count.ToString(),
                PerkNames = perkNames,
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
