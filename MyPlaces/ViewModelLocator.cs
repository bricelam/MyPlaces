using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPlaces.ViewModels;

namespace MyPlaces
{
    class ViewModelLocator
    {
        static readonly ServiceProvider _services;

        static ViewModelLocator()
        {
            _services = new ServiceCollection()
                .AddSingleton<IConfiguration>(
                    new ConfigurationBuilder()
                        .AddUserSecrets<ViewModelLocator>()
                        .Build())
                .AddSingleton<MainViewModel>()
                .BuildServiceProvider(validateScopes: true);
        }

        public MainViewModel Main
            => _services.GetRequiredService<MainViewModel>();

        public static void Cleanup()
            => _services.Dispose();
    }
}
