// Copyright (c) Microsoft. All rights reserved.

// This is the main entry point of the application.
// It sets up the WebAssembly host and configures the services needed by the application.

// Create a default WebAssembly host builder.
var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add root components to the builder.
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure application settings.
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection(nameof(AppSettings)));

// Add an HTTP client for the API.
builder.Services.AddHttpClient<ApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

// Add various services to the application.
builder.Services.AddScoped<OpenAIPromptQueue>();
builder.Services.AddLocalStorageServices();
builder.Services.AddSessionStorageServices();
builder.Services.AddSpeechSynthesisServices();
builder.Services.AddSpeechRecognitionServices();
builder.Services.AddMudServices();

// Import a JavaScript module.
await JSHost.ImportAsync(
    moduleName: nameof(JavaScriptModule),
    moduleUrl: $"../js/iframe.js?{Guid.NewGuid()}" /* cache bust */);

// Build the host and run the application.
var host = builder.Build();
await host.RunAsync();
