// Copyright (c) Microsoft. All rights reserved.

/// <summary>
/// Main entry point for the application.
/// </summary>
/// <param name="args">Command line arguments.</param>
var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add the root components to the builder.
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure the application settings.
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection(nameof(AppSettings)));

// Add an HTTP client for the API.
builder.Services.AddHttpClient<ApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

// Add services to the builder.
builder.Services.AddScoped<OpenAIPromptQueue>();
builder.Services.AddLocalStorageServices();
builder.Services.AddSessionStorageServices();
builder.Services.AddSpeechSynthesisServices();
builder.Services.AddSpeechRecognitionServices();
builder.Services.AddMudServices();

// Import the JavaScript module.
await JSHost.ImportAsync(
    moduleName: nameof(JavaScriptModule),
    moduleUrl: $"../js/iframe.js?{Guid.NewGuid()}" /* cache bust */);

// Build and run the host.
var host = builder.Build();
await host.RunAsync();
