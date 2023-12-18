// Copyright (c) Microsoft. All rights reserved.

// This is the main entry point of the application.
// It configures and builds the WebAssembly host.

// Create a default WebAssembly host builder.
var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add root components to the builder.
builder.RootComponents.Add<App>("#app"); // Add the main app component.
builder.RootComponents.Add<HeadOutlet>("head::after"); // Add the head outlet component.

// Configure services.
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection(nameof(AppSettings))); // Configure app settings.
builder.Services.AddHttpClient<ApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); // Add HTTP client with base address.
});
builder.Services.AddScoped<OpenAIPromptQueue>(); // Add OpenAI prompt queue service.
builder.Services.AddLocalStorageServices(); // Add local storage services.
builder.Services.AddSessionStorageServices(); // Add session storage services.
builder.Services.AddSpeechSynthesisServices(); // Add speech synthesis services.
builder.Services.AddSpeechRecognitionServices(); // Add speech recognition services.
builder.Services.AddMudServices(); // Add MudBlazor services.

// Import JavaScript module.
await JSHost.ImportAsync(
    moduleName: nameof(JavaScriptModule),
    moduleUrl: $"../js/iframe.js?{Guid.NewGuid()}" /* cache bust */);

// Build the host and run it.
var host = builder.Build();
await host.RunAsync();
