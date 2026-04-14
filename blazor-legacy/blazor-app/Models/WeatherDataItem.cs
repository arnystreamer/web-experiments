namespace BlazorAppTest.Models;

public record WeatherDataItem(int Id, DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public record WeatherDataCollectionResult(WeatherDataItem[] Items)
{
    public int Count = Items.Length;
}