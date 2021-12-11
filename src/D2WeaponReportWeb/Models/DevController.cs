namespace D2WeaponReportWeb.Models
{
    using Microsoft.AspNetCore.Mvc;

    public class DevController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
