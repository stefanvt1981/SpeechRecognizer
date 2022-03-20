
namespace BaseCommandRecognizer
{
    public interface ICommandRecognizer
    {
        event Action<string> Recognized;
        void Dispose();
        Task StartListeningForCommandsAsync();
        Task StopListeningForCommandsAsync();
    }
}