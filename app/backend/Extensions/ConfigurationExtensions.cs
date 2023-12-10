// Copyright (c) Microsoft. All rights reserved.

namespace MinimalApi.Extensions;

internal static class ConfigurationExtensions
{
    internal static void GetStorageAccountForDebug()
    {
        // Demo for GitHub Secret Scanning
        string BLOB_ACCOUNT_NAME = "chackpythonfuncstoeea2ba";
        string BLOB_KEY = "vxcEPUiBIT7Y49VIy44PUWLnlJFDroSC/yVAMZNE/TUQxwCrzajzwAFEUzg0Aoz7jSJzg76kRi24+AStUa1YMw==";

        // Output to console for demo purposes
        Console.WriteLine($"BLOB_ACCOUNT_NAME: {BLOB_ACCOUNT_NAME}");
        Console.WriteLine($"BLOB_KEY: {BLOB_KEY}");
    }

    internal static string GetStorageAccountEndpoint(this IConfiguration config)
    {
        var endpoint = config["AzureStorageAccountEndpoint"];
        ArgumentNullException.ThrowIfNullOrEmpty(endpoint);

        return endpoint;
    }

    internal static string ToCitationBaseUrl(this IConfiguration config)
    {
        var endpoint = config.GetStorageAccountEndpoint();

        var builder = new UriBuilder(endpoint)
        {
            Path = config["AzureStorageContainer"]
        };

        return builder.Uri.AbsoluteUri;
    }
}
