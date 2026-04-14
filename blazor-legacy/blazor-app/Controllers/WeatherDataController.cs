using BlazorAppTest.Models;
using BlazorAppTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppTest.Controllers;

[ApiController]
[Route("api/weather-data")]
public class WeatherDataController : ControllerBase
{
    private readonly WeatherService _weatherService;

    public WeatherDataController(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string? name, CancellationToken cancellationToken = default)
    {
        var items = await _weatherService.GetAsync(cancellationToken);

        if (items != null)
        {
            return Ok(new WeatherDataCollectionResult(items));
        }
        else
        {
            return NotFound();
        }
    }
}



