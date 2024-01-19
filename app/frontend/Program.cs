// アプリケーションのメインエントリーポイント
var builder = WebAssemblyHostBuilder.CreateDefault(args);

// ルートコンポーネントの追加
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// アプリケーション設定の設定
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection(nameof(AppSettings)));

// HTTPクライアントの追加
builder.Services.AddHttpClient<ApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

// OpenAIプロンプトキューの追加
builder.Services.AddScoped<OpenAIPromptQueue>();

// ローカルストレージサービスの追加
builder.Services.AddLocalStorageServices();

// セッションストレージサービスの追加
builder.Services.AddSessionStorageServices();

// 音声合成サービスの追加
builder.Services.AddSpeechSynthesisServices();

// 音声認識サービスの追加
builder.Services.AddSpeechRecognitionServices();

// MudBlazorサービスの追加
builder.Services.AddMudServices();

// JavaScriptモジュールのインポート
await JSHost.ImportAsync(
    moduleName: nameof(JavaScriptModule),
    moduleUrl: $"../js/iframe.js?{Guid.NewGuid()}" /* cache bust */);

// ホストのビルドとアプリケーションの実行
var host = builder.Build();
await host.RunAsync();
