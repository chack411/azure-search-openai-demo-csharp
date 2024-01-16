var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Root components
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// App settings
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection(nameof(AppSettings)));

// HTTP client
builder.Services.AddHttpClient<ApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

// OpenAI prompt queue
builder.Services.AddScoped<OpenAIPromptQueue>();

// Local storage services
builder.Services.AddLocalStorageServices();

// Session storage services
builder.Services.AddSessionStorageServices();

// Speech synthesis services
builder.Services.AddSpeechSynthesisServices();

// Speech recognition services
builder.Services.AddSpeechRecognitionServices();

// MudBlazor services
builder.Services.AddMudServices();

// JavaScript module
await JSHost.ImportAsync(
    moduleName: nameof(JavaScriptModule),
    moduleUrl: $"../js/iframe.js?{Guid.NewGuid()}" /* cache bust */);

// Build and run
var host = builder.Build();
await host.RunAsync();
