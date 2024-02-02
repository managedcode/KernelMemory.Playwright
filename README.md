# KernelMemory.Playwright

KernelMemory.Playwright is an open-source project that provides methods to import websites as text or images into memory. It leverages the power of the Playwright library to interact with web pages programmatically.

[![NuGet Package](https://img.shields.io/nuget/v/ManagedCode.KernelMemory.Playwright.svg)](https://www.nuget.org/packages/ManagedCode.KernelMemory.Playwright)

## Features

- Import web pages as text: This feature allows you to fetch the entire text content of a web page and store it in memory.
- Import web pages as images: This feature enables you to take a screenshot of a web page and store it in memory.

## How to Use



1. Install the package into your project.

You can install the `ManagedCode.KernelMemory.Playwright` NuGet package by running the following command in your terminal:

```bash
dotnet add package ManagedCode.KernelMemory.Playwright
```

Add Playwright to your services. 
```csharp
services.WithPlaywright();
```

2. Use the `ImportWebPageAsTextWithPlaywrightAsync` or `ImportWebPageAsImageWithPlaywrightAsync` methods to import a web page as text or image respectively.

## Example

```csharp
// Import a web page as text
string documentId = await memory.ImportWebPageAsTextWithPlaywrightAsync("https://example.com");

// Import a web page as an image
string documentId = await memory.ImportWebPageAsImageWithPlaywrightAsync("https://example.com");

// Get the content of a web page as text
string pageText = await memory.GetWebPageContentAsTextAsync("https://example.com");

// Get the content of a web page as an image
byte[] pageImage = await memory.GetWebPageContentAsImageAsync("https://example.com");
```
