namespace Restaurants.Domain.Interfaces;

public interface IBlobStorageService
{
    string? GetBlobSasUrl(string? blobUrl);
    public Task<string> UploadToBlobAsync(Stream file , string Filename);
}
