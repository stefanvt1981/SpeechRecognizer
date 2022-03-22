using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRecognizer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConsoleCommandRecognizer(this IServiceCollection self)
            => self.AddSingleton<IConsoleCommandRecognizer, ConsoleCommandRecognizer>();
    }
}
