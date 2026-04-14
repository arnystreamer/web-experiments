using BlazorAppTest.Models;

namespace BlazorAppTest.Services;

public class WeatherService
{
    private WeatherDataItem[]? _weatherDataItems;
    
    public void Initialize()
    {
        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        
        _weatherDataItems = Enumerable.Range(1, 5).Select(index => new WeatherDataItem(
                index, 
                startDate.AddDays(index), 
                Random.Shared.Next(-20, 55), 
                summaries[Random.Shared.Next(summaries.Length)]))
            .ToArray();
    }
    
    public async Task<WeatherDataItem[]?> GetAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(500, cancellationToken);
        
        return _weatherDataItems;
    }
}