﻿namespace SandboxWeb.Controllers
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using DestinyLib;
    using DestinyLib.Database;
    using DestinyLib.Scenarios;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using SandboxWeb.Models;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly WorldSqlContentProvider worldSqlContentProvider;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            // TODO: DEPENDENCY INJECTION and CACHING
            var dbPath = new FileInfo(LibEnvironment.GetDatabaseFilePath("world_sql_content"));
            var worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));
            this.worldSqlContentProvider = new WorldSqlContentProvider(worldSqlContent, ProviderOptions.ScenarioDefault);
        }

        /// <summary>
        /// 
        /// https://localhost:44365/Home/Index/2891672170
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Index(string id)
        {
            var weapons = this.worldSqlContentProvider.GetSearchableWeapons();
            var weaponNames = weapons.Select(x => x.Name);

            var model = new PermutationsViewModel
            { 
                WeaponNames = string.Join(",", weaponNames),
            };

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
                var searchRecord = SearchForWeaponScenario.Run(id, SearchForWeaponScenario.SearchType.StringContains).Single();
                var hashid = searchRecord.HashId;

                model.Summary = GetSummaryDetails(hashid);
            }

            return View(model);
        }

        private PermutationsViewModel.SummaryDetails GetSummaryDetails(uint hash)
        {
            var definition = GetWeaponDefinitionScenario.Run(hash);
            var weaponSummary = GetWeaponAnalysisScenario.Run(hash);

            var perkNames = weaponSummary.Permutations.OrderByDescending(x => x.PerkSum).Select(x => x.ToDisplayString()).ToList();

            return new()
            {
                Name = definition.MetaData.Name,
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
