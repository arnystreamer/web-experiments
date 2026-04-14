using System.Text.Json.Serialization;
using BlazorAppTest;
using BlazorAppTest.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Prometheus;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging(bld => bld.AddSerilog(dispose: true));

builder.Services
    .AddMvc(opts => opts.EnableEndpointRouting = false)
    .AddJsonOptions(opts => opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

//builder.Services.AddRazorComponents()
//    .AddInteractiveServerComponents();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHealthChecks();

builder.Services.AddSingleton<WeatherService>();
builder.Services.AddHostedService<HeartBeatHostedService>();

var app = builder.Build();

app.Services.GetRequiredService<WeatherService>().Initialize();

var pathBase = builder.Configuration["PathBase"];
Log.Logger.Information("Path base is {PathBase}", pathBase);
if (!string.IsNullOrEmpty(pathBase))
{
    app.UsePathBase(pathBase);
}

app.UseStaticFiles();
app.UseRouting();

app.UseForwardedHeaders(new ForwardedHeadersOptions()
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedFor
});

app.UseMvcWithDefaultRoute();

#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("health");
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
    endpoints.MapMetrics("metrics");

    endpoints.MapControllers();
});
#pragma warning restore ASP0014

//app.MapRazorComponents<App>()
//    .AddInteractiveServerRenderMode();

app.Run();