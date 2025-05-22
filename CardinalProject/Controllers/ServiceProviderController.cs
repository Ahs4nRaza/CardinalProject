using Microsoft.AspNetCore.Mvc;

namespace CardinalProject.Controllers
{
    public class ServiceProviderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
