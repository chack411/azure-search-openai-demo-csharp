// Copyright (c) Microsoft. All rights reserved.

// This is the main entry point for the application.
// It sets up the WebAssembly host, configures services, and starts the application.

// Create a WebAssemblyHostBuilder with default configuration.
var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add root components to the builder.
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure services.
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection(nameof(AppSettings))); // Configure AppSettings using the configuration section.
builder.Services.AddHttpClient<ApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); // Add an HttpClient for ApiClient with the base address set to the host environment base address.
});
builder.Services.AddScoped<OpenAIPromptQueue>(); // Add OpenAIPromptQueue to the service collection.
builder.Services.AddLocalStorageServices(); // Add local storage services to the service collection.
builder.Services.AddSessionStorageServices(); // Add session storage services to the service collection.
builder.Services.AddSpeechSynthesisServices(); // Add speech synthesis services to the service collection.
builder.Services.AddSpeechRecognitionServices(); // Add speech recognition services to the service collection.
builder.Services.AddMudServices(); // Add MudBlazor services to the service collection.

// Import JavaScript module.
await JSHost.ImportAsync(
    moduleName: nameof(JavaScriptModule),
    moduleUrl: $"../js/iframe.js?{Guid.NewGuid()}" /* cache bust */); // Import JavaScriptModule from the specified URL with a cache busting query string.

// Build the host and run the application.
var host = builder.Build();
await host.RunAsync();
