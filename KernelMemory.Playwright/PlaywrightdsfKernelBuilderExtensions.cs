using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.KernelMemory;
using Microsoft.Playwright;

namespace KernelMemory.Playwright;

public static class PlaywrightPageAsImageKernelBuilderExtensions
{
    public static async Task<string> ImportWebPageWithPlaywrightAsync(
        this IKernelMemory memory, 
        string url, 
        string? fileName = null,
        string? documentId = null,
        TagCollection? tags = null,
        string? index = null,
        IEnumerable<string>? steps = null,
        BrowserTypeLaunchOptions? browserTypeLaunchOptions = default,
        BrowserNewContextOptions? browserNewContextOptions = default, 
        CancellationToken cancellationToken = default)
    {
        using var playwright = await Microsoft.Playwright.Playwright.CreateAsync()
            .ConfigureAwait(false);
        
        await using var browser = await playwright.Chromium.LaunchAsync(browserTypeLaunchOptions)
            .ConfigureAwait(false);

        await using var context = await browser.NewContextAsync(browserNewContextOptions)
            .ConfigureAwait(false);
        
        return await ImportWebPageWithPlaywrightInternalAsync(memory, context, url, fileName, documentId, tags, index, steps, cancellationToken)
            .ConfigureAwait(false);
    }
    
    public static async Task<string[]> ImportWebPageWithPlaywrightAsync(
        this IKernelMemory memory, 
        string[] urls, 
        string? fileName = null,
        string? documentId = null,
        TagCollection? tags = null,
        string? index = null,
        IEnumerable<string>? steps = null,
        BrowserTypeLaunchOptions? browserTypeLaunchOptions = default,
        BrowserNewContextOptions? browserNewContextOptions = default, 
        CancellationToken cancellationToken = default)
    {
        using var playwright = await Microsoft.Playwright.Playwright.CreateAsync()
            .ConfigureAwait(false);
        
        await using var browser = await playwright.Chromium.LaunchAsync(browserTypeLaunchOptions)
            .ConfigureAwait(false);

        await using var context = await browser.NewContextAsync(browserNewContextOptions)
            .ConfigureAwait(false);
        
        List<string> results = new(urls.Length);

        foreach (var url in urls)
        {
            var result= await ImportWebPageWithPlaywrightInternalAsync(memory, context, url, fileName, documentId, tags, index, steps, cancellationToken)
                .ConfigureAwait(false);
            
            results.Add(result);
        }

        return results.ToArray();
    }
    
    private static async Task<string> ImportWebPageWithPlaywrightInternalAsync(
        IKernelMemory memory, 
        IBrowserContext browserContext,
        string url, 
        string? fileName = null,
        string? documentId = null,
        TagCollection? tags = null,
        string? index = null,
        IEnumerable<string>? steps = null,
        CancellationToken cancellationToken = default)
    {
        var text = await GetWebPageAsImageAsync(browserContext, url, cancellationToken)
            .ConfigureAwait(false);
        
        var stream = new System.IO.MemoryStream(text);
        
        return await memory.ImportDocumentAsync(stream, fileName, documentId, tags, index, steps, cancellationToken)
            .ConfigureAwait(false);
    }
    
    
    private static async Task<byte[]> GetWebPageAsImageAsync(
        IBrowserContext browserContext,
        string url, 
        CancellationToken cancellationToken = default)
    {
        var page = await browserContext.NewPageAsync().ConfigureAwait(false);
        try
        {
            await page.GotoAsync(url);
            await page.EvaluateAsync("window.scrollTo(0, document.body.scrollHeight)");
            var image = await page.ScreenshotAsync(new PageScreenshotOptions()
            {
                FullPage = true,
                Quality = 95,
                Type = ScreenshotType.Jpeg,
                Scale = ScreenshotScale.Css
            });
            return image;
        }
        finally
        {
            await page.CloseAsync();
        }
    }
}