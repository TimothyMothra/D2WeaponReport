namespace SandboxWeb.Models
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
