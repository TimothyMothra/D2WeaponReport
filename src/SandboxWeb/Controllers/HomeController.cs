namespace SandboxWeb.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using DestinyLib.Scenarios;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using SandboxWeb.Models;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// https://localhost:44365/Home/Index/2891672170
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Index(string id)
        {
            PermutationsModel model;

            if (id == null)
            {
                model = new PermutationsModel { Name = "No id provided" };
            }
            else if (UInt32.TryParse(id, out uint hash))
            {
                try
                {
                    var definition = GetWeaponDefinitionScenario.Run(hash);
                    var weaponSummary = GetWeaponAnalysisScenario.Run(hash);

                    model = new PermutationsModel
                    {
                        Name = definition.MetaData.Name,
                        BaseValue = weaponSummary.Base.ToString(),
                        Values = weaponSummary.PermutationsAsString(),
                        PermutationsCount = weaponSummary.Permutations.Count().ToString(),
                    };
                }
                catch (Exception ex)
                {
                    model = new PermutationsModel { Name = ex.ToString() };
                }
            }
            else
            {
                model = new PermutationsModel { Name = id };
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
