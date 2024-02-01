// Copyright (c) Microsoft. All rights reserved.

// アプリケーションのメインエントリーポイント
var builder = WebAssemblyHostBuilder.CreateDefault(args);

// ルートコンポーネントの追加
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// アプリケーション設定の設定
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection(nameof(AppSettings)));

// APIクライアントのためのHTTPクライアントの追加
builder.Services.AddHttpClient<ApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

// スコープ内サービスの追加
builder.Services.AddScoped<OpenAIPromptQueue>();
builder.Services.AddLocalStorageServices();
builder.Services.AddSessionStorageServices();
builder.Services.AddSpeechSynthesisServices();
builder.Services.AddSpeechRecognitionServices();
builder.Services.AddMudServices();

// JavaScriptモジュールのインポート
await JSHost.ImportAsync(
    moduleName: nameof(JavaScriptModule),
    moduleUrl: $"../js/iframe.js?{Guid.NewGuid()}" /* キャッシュバスト */);

// ホストのビルドとアプリケーションの実行
var host = builder.Build();
await host.RunAsync();
