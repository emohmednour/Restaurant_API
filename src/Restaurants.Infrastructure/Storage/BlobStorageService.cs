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

   
}
