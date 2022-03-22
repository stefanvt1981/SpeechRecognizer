using BaseCommandRecognizer;
using Microsoft.Extensions.Logging;

namespace ConsoleRecognizer
{
    public class ConsoleCommandRecognizer : CommandRecognizer, IConsoleCommandRecognizer
    {
        private bool _running = false;

        public ConsoleCommandRecognizer(ILogger<ConsoleCommandRecognizer> logger) : base(logger)
        {

        }

        public override event Action<string> Recognized = (command) => { };

        public override void Dispose()
        {
            // Nothing yet
        }

        public override Task StartListeningForCommandsAsync()
        {
            _running = true;

            while (_running)
            {
                var command = Console.ReadKey();
                Recognized(command.Key.ToString());
            }

            return Task.CompletedTask;
        }

        public override Task StopListeningForCommandsAsync()
        {
            _running = false;
            return Task.CompletedTask;
        }
    }
}