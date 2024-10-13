using Asp.Versioning;
using FeatureManagementPoc.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FeatureManagementPoc.Web.Areas.V1.Controllers
{
    [Area("V1")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("Index", "V1");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
