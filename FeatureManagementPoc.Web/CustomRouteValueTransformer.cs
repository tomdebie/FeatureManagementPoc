using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.FeatureManagement;

namespace FeatureManagementPoc.Web;

public class CustomRouteValueTransformer : DynamicRouteValueTransformer
{
    private readonly IFeatureManager _featureManager;
    private const string VersionCookieName = "Feature_Version";

    public CustomRouteValueTransformer(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    public override async ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
    {
        // If the area is already defined, don't overwrite it
        if (values.ContainsKey("area") && !string.IsNullOrEmpty(values["area"]?.ToString()))
        {
            return values;
        }

        // Check if the version has already been assigned via cookie
        if (!httpContext.Request.Cookies.TryGetValue(VersionCookieName, out var version))
        {
            // Determine version based on feature flag
            bool isNewVersion = await _featureManager.IsEnabledAsync("Test");

            // Set the version in the cookie
            version = isNewVersion ? "V2" : "V1";
            httpContext.Response.Cookies.Append(VersionCookieName, version, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddHours(6),
                SameSite = SameSiteMode.Lax // Adjust based on your requirements
            });
        }

        values["area"] = version;

        return new RouteValueDictionary(values);
    }
}