using Microsoft.Extensions.Logging;

namespace BaseCommandRecognizer
{
    public abstract class CommandRecognizer : ICommandRecognizer, IDisposable
    {
        protected readonly ILogger _logger;

        public CommandRecognizer(ILogger logger)
        {
            _logger = logger;
        }

        public abstract event Action<string> Recognized;

        public abstract void Dispose();
        public abstract Task StartListeningForCommandsAsync();
        public abstract Task StopListeningForCommandsAsync();
    }
}