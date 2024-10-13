using FeatureManagementPoc.Web;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHealthChecks();
builder.Services.AddFeatureManagement(builder.Configuration.GetSection("Features"));
builder.Services.AddScoped<CustomRouteValueTransformer>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(routes =>
{
    routes.MapControllerRoute(
        name: "areas",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );

    routes.MapDynamicControllerRoute<CustomRouteValueTransformer>("{controller=Home}/{action=Index}/{id?}");
    routes.MapHealthChecks("/_health", new HealthCheckOptions
    {
        AllowCachingResponses = false,
        ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
    }).ShortCircuit();
});

//app.MapDynamicControllerRoute<CustomTransformer>("{controller=Home}/{action=Index}/{id?}");

app.Run();
