using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpeechRecognizer;

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("appsettings.json");
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();   
    })
    .ConfigureServices((hostContext, services) =>
    {
        var configurationRoot = hostContext.Configuration;
        services.Configure<SpeechCommandRecognizerOptions>(
            configurationRoot.GetSection(nameof(SpeechCommandRecognizerOptions)));

        services.AddHostedService<ConsoleHostedService>();

        services.AddSpeechCommandRecognizer();
    })
    .RunConsoleAsync();


internal sealed class ConsoleHostedService : IHostedService
{
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly ISpeechCommandRecognizer _speechCommandRecognizer;

    public ConsoleHostedService(
        ILogger<ConsoleHostedService> logger,
        IHostApplicationLifetime appLifetime,
        ISpeechCommandRecognizer speechCommandRecognizer)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _speechCommandRecognizer = speechCommandRecognizer;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

        _appLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                try
                {
                    _logger.LogInformation("Hello World!");

                    // Simulate real work is being done
                    // await Task.Delay(1000);

                    _speechCommandRecognizer.Recognized += _speechCommandRecognizer_Recognized;

                    _logger.LogInformation("Listening...");

                    await _speechCommandRecognizer.StartListeningForCommandsAsync();

                    
                    Console.WriteLine("Press enter to quit.");
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        await _speechCommandRecognizer.StopListeningForCommandsAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled exception!");
                }
                finally
                {
                    // Stop the application once the work is done
                    _appLifetime.StopApplication();
                }
            });
        });

        return Task.CompletedTask;
    }

    private void _speechCommandRecognizer_Recognized(string command)
    {
        _logger.LogInformation($"Command recognized: {command}");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}