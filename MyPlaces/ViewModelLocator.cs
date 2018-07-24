using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPlaces.Models;
using MyPlaces.ViewModels;

namespace MyPlaces
{
    class ViewModelLocator
    {
        static readonly ServiceProvider _services;

        static ViewModelLocator()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<ViewModelLocator>()
                .Build();

            var connectionString = new SqliteConnectionStringBuilder
            {
                // TODO: AppData?
                DataSource = "MyPlaces.db",
                Cache = SqliteCacheMode.Shared
            }.ToString();

            _services = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddDbContext<PlaceContext>(
                    x => x.UseSqlite(connectionString))
                .AddSingleton<MainViewModel>()
                .BuildServiceProvider(validateScopes: true);
        }

        public MainViewModel Main
            => _services.GetRequiredService<MainViewModel>();

        public static void Cleanup()
            => _services.Dispose();
    }
}
