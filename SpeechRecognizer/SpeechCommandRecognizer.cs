using BaseCommandRecognizer;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SpeechRecognizer
{
    public class SpeechCommandRecognizer : CommandRecognizer, IDisposable, ISpeechCommandRecognizer
    {
        private readonly string SUBSCRIPTION_KEY;
        private readonly string SERVICE_REGION;

        private Microsoft.CognitiveServices.Speech.SpeechRecognizer _speechRecognizer;
        

        private bool disposedValue;

        public override event Action<string> Recognized = (recognizedCommand) => { };

        public SpeechCommandRecognizer(IOptions<SpeechCommandRecognizerOptions> speechRecognizerOptions, ILogger<SpeechCommandRecognizer> logger) :
            base(logger)
        {
            SUBSCRIPTION_KEY = speechRecognizerOptions.Value.SubscriptionKey;
            SERVICE_REGION = speechRecognizerOptions.Value.ServiceRegion;            
                       

            var config = SpeechConfig.FromSubscription(SUBSCRIPTION_KEY, SERVICE_REGION);
            _speechRecognizer = new Microsoft.CognitiveServices.Speech.SpeechRecognizer(config);

            _speechRecognizer.Recognized += _speechRecognizer_Recognized;
            _speechRecognizer.SessionStarted += _speechRecognizer_SessionStarted;
            _speechRecognizer.SessionStopped += _speechRecognizer_SessionStopped;
            _speechRecognizer.Canceled += _speechRecognizer_Canceled;
            _speechRecognizer.SpeechStartDetected += _speechRecognizer_SpeechStartDetected;
            _speechRecognizer.SpeechEndDetected += _speechRecognizer_SpeechEndDetected;

            _logger.LogDebug($"SpeechCommandRecognizer ctor completed.");
        }

        private void _speechRecognizer_SpeechEndDetected(object? sender, RecognitionEventArgs e)
        {
            _logger.LogDebug($"SpeechEndDetected: {e.SessionId}");
        }

        private void _speechRecognizer_SpeechStartDetected(object? sender, RecognitionEventArgs e)
        {
            _logger.LogDebug($"SpeechStartDetected: {e.SessionId}");
        }

        private void _speechRecognizer_Canceled(object? sender, SpeechRecognitionCanceledEventArgs e)
        {
            _logger.LogDebug($"Canceled: {e.SessionId} \nReason: {e.Reason}");
        }

        private void _speechRecognizer_SessionStopped(object? sender, SessionEventArgs e)
        {
            _logger.LogInformation($"SessionStopped: {e.SessionId}");
        }

        private void _speechRecognizer_SessionStarted(object? sender, SessionEventArgs e)
        {
            _logger.LogInformation($"SessionStarted: {e.SessionId}");
        }

        private void _speechRecognizer_Recognized(object? sender, SpeechRecognitionEventArgs e)
        {
            _logger.LogDebug($"SpeechStartDetected: {e.SessionId} \nResult: {e.Result.Text}");
            Recognized(e.Result.Text);
        }

        public override async Task StartListeningForCommandsAsync()
        {
            await _speechRecognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
        }

        public override async Task StopListeningForCommandsAsync()
        {
            await _speechRecognizer.StopKeywordRecognitionAsync().ConfigureAwait(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _speechRecognizer?.Dispose();
                }
                disposedValue = true;
            }
        }

        public override void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
