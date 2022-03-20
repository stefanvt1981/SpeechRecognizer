using BaseCommandRecognizer;
using Microsoft.Extensions.Logging;

namespace ConsoleRecognizer
{
    public class ConsoleCommandRecognizer : CommandRecognizer
    {
        public ConsoleCommandRecognizer(ILogger<ConsoleCommandRecognizer> logger) : base(logger)
        {
            //Console.
        }

        public override event Action<string> Recognized;

        public override void Dispose()
        {
            // Nothing yet
        }

        public override Task StartListeningForCommandsAsync()
        {
            throw new NotImplementedException();
        }

        public override Task StopListeningForCommandsAsync()
        {
            throw new NotImplementedException();
        }
    }
}