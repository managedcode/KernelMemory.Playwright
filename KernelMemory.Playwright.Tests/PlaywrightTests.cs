using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;
using Xunit;

namespace KernelMemory.Playwright.Tests;

public class PlaywrightTests
{
    [Fact]
    public async Task OpenPage()
    {
        await PlaywrightInstaller.Install();

        using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync();

        await using var context = await browser.NewContextAsync();

        var page = await context.NewPageAsync();

        //await page.GotoAsync("https://www.managed-code.com");  
        await page.GotoAsync("https://devblogs.microsoft.com/ise/api-testing-using-http-files/");

        //scroll to bottom
        await page.EvaluateAsync("window.scrollTo(0, document.body.scrollHeight)");

        // Get the clear text of the page
        var clearText = await page.InnerTextAsync("body");

        clearText.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task OpenImagePage()
    {
        await PlaywrightInstaller.Install();

        using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync();

        await using var context = await browser.NewContextAsync();

        var page = await context.NewPageAsync();
        
        await page.GotoAsync("https://devblogs.microsoft.com/ise/api-testing-using-http-files/");

        //scroll to bottom
        await page.EvaluateAsync("window.scrollTo(0, document.body.scrollHeight)");
        

        var image = await page.ScreenshotAsync(new PageScreenshotOptions
        {
            FullPage = true,
            Quality = 95,
            Type = ScreenshotType.Jpeg,
            Scale = ScreenshotScale.Css
        });

        await File.WriteAllBytesAsync("test.jpg", image);
    }
}