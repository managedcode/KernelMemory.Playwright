using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace KernelMemory.Playwright;

internal class PlaywrightInstallerBackgroundService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return PlaywrightInstaller.Install();
    }
}