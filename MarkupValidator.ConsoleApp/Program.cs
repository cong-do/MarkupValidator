using Microsoft.Extensions.DependencyInjection;
using MarkupValidator.ConsoleApp.Services;
using MarkupValidator.Core;

namespace MarkupValidator.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Load dependency injection
            var services = BuildServices();
            var appService = services.GetRequiredService<IAppService>();

#if DEBUG
            // DEBUGGING PURPOSES: prefill argument with test XML input
            args = ["<Design><Code>hello world</Code></Design>"];
#endif

            // Run validator application
            appService.Run(args);
        }

        private static ServiceProvider BuildServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IValidator, XmlValidator>()
                .AddSingleton<IAppService, AppService>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}