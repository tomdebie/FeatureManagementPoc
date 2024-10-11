using Asp.Versioning;
using FeatureManagementPoc.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FeatureManagementPoc.Web.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
