using display.Drawer;
using logic.BranchManager;
using logic.SearchManager;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddScoped<IBranchManager, BranchManager>();
        services.AddScoped<ISearchManager, SearchManager>();
        services.AddScoped<IDrawer, Drawer>();
        services.AddScoped<GitFuzz>();
    })
    .Build();

var gf = ActivatorUtilities.CreateInstance<GitFuzz>(host.Services);

gf.Run(args);
