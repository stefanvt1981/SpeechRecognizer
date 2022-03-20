using Microsoft.Extensions.DependencyInjection;

namespace SpeechRecognizer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSpeechCommandRecognizer(this IServiceCollection self)
            => self.AddSingleton<ISpeechCommandRecognizer, SpeechCommandRecognizer>();
    }
}
