namespace BlazorAppTest;

public class HeartBeatHostedService : BackgroundService
{
    private readonly ILogger _logger;

    public HeartBeatHostedService(ILogger<HeartBeatHostedService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int counter = 0;
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
            Interlocked.Increment(ref counter);

            if (counter % 5 == 0)
            {
                _logger.LogInformation("Number of seconds passed from start: {NumberOfSeconds}", counter);
            }
        }
    }
}