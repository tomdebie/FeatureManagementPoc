using Microsoft.AspNetCore.Mvc;

namespace FeatureManagementPoc.Web.Areas.V1.Controllers;

[Area("V1")]
public class PageController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
