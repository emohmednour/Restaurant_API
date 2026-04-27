using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client.Extensions.Msal;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Configuration;

namespace Restaurants.Infrastructure.Storage;

public class BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettingsOptions) : IBlobStorageService
{
    private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettingsOptions.Value;
    public async Task<string> UploadToBlobAsync(Stream file, string Filename)
    {

        //connect to account storage
        var client = new BlobServiceClient(_blobStorageSettings.ConnectionString);

        //connect to Container 
        var container = client.GetBlobContainerClient(_blobStorageSettings.LogosContainerName);

        //connect file نقسه
        var blob = container.GetBlobClient(Filename);

        await blob.UploadAsync(file);

        return blob.Uri.ToString();

    }
    public string? GetBlobSasUrl(string? blobUrl)
    {
        if (blobUrl == null) return null;

        var sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = _blobStorageSettings.LogosContainerName,
            Resource = "b",
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30),
            BlobName = GetBlobNameFromUrl(blobUrl)
        };


        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);


        var sasToken = sasBuilder
            .ToSasQueryParameters(new StorageSharedKeyCredential(blobServiceClient.AccountName, _blobStorageSettings.AccountKey))
            .ToString();

        return $"{blobUrl}?{sasToken}";
        // blob: https://restaurantssadev.blob.core.windows.net/ logos/ logo-fun.jfif
        // sas: sp=r&st=2024-02-19T08:18:05Z&se=2024-02-19T16:18:05Z&spr=https&sv=2022-11-02&sr=b&sig=bB2hSZtqsbImIuwM7CYMTYSXMrEt5u5K6RJ1EbjrxGA%3D
    }

    private string GetBlobNameFromUrl(string blobUrl)
    {
        var uri = new Uri(blobUrl);
        return uri.Segments.Last();
    }


}
