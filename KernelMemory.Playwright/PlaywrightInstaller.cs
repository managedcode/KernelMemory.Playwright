using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;

namespace KernelMemory.Playwright;

internal static class PlaywrightInstaller
{
    private static Task<bool> InstallInternal(string command)
    {
        var taskCompletionSource = new TaskCompletionSource<bool>();
        _ = Task.Factory.StartNew(() =>
        {
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger(nameof(PlaywrightInstaller));
            logger.LogInformation("Installing Playwright...");
            var exitCode = Program.Main(new[] { command });
            taskCompletionSource.SetResult(exitCode == 0);
            logger.LogInformation("Playwright installed; Status: {ExitCode}", exitCode == 0);
        }, TaskCreationOptions.LongRunning);

        return taskCompletionSource.Task;
    }

    public static Task<bool> InstallChrome()
    {
        return InstallInternal("install chrome");
    }

    public static Task<bool> Install()
    {
        return InstallInternal("install");
    }
}