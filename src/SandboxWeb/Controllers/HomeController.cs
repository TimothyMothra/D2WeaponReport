namespace SandboxWeb.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using DestinyLib.Database;
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

            var model = new PermutationsViewModel(weaponNames);

            if (id == null)
            {
                model.Summary = null;
            }
            else if (UInt32.TryParse(id, out uint hash))
            {
                model.Summary = GetSummaryDetails(hash);
            }
            else
            {
                var searchResults = SearchForWeaponScenario.Run(id, SearchForWeaponScenario.SearchType.Regex).ToList();
                
                if (searchResults.Count == 1)
                {
                    var hashid = searchResults[0].HashId;

                    model.Summary = GetSummaryDetails(hashid);
                }
                else if (searchResults.Count > 1 )
                {
                    var displayResults = new List<PermutationsViewModel.SearchResult>(searchResults.Count);

                    foreach (var result in searchResults)
                    {
                        displayResults.Add(new PermutationsViewModel.SearchResult
                        {
                            Name = result.Name,
                            Id = result.HashId,
                            IconUri = result.GetIconUri().AbsoluteUri,
                        });
                    }

                    model.MultipleResults = displayResults;
                }
                else
                {
                    model.Error = "No Results Found";
                }
            }

            return View(model);
        }

        private PermutationsViewModel.SummaryDetails GetSummaryDetails(uint hash)
        {
            var definition = GetWeaponDefinitionScenario.Run(hash);
            var weaponSummary = GetWeaponAnalysisScenario.Run(hash);

            var perkNames = weaponSummary.Permutations.OrderByDescending(x => x.MaxPoints).Select(x => x.ToDisplayString()).ToList();

            return new()
            {
                Name = definition.MetaData.Name,
                IconUri = definition.MetaData.GetIconUri().AbsoluteUri,
                ScreenshotUri = definition.MetaData.GetScreenshotUri().AbsoluteUri,
                BaseValue = weaponSummary.Base.ToString(),
                Values = weaponSummary.PermutationsAsString(),
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
