using Microsoft.Extensions.DependencyInjection;
using Microsoft.KernelMemory;

namespace KernelMemory.Playwright;

public static class PlaywrightKernelMemoryBuilderExtensions
{
    public static IServiceCollection WithPlaywright(this IServiceCollection collection)
    {
        collection.AddHostedService<PlaywrightInstallerBackgroundService>();
        return collection;
    }

    public static IKernelMemoryBuilder WithPlaywright(this IKernelMemoryBuilder builder)
    {
        builder.Services.AddHostedService<PlaywrightInstallerBackgroundService>();
        return builder;
    }
}