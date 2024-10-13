using Microsoft.AspNetCore.Mvc;

namespace FeatureManagementPoc.Web.Controllers;

[Area("V2")]
public class PageController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
