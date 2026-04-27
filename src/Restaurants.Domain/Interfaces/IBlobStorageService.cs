namespace Restaurants.Domain.Interfaces;

public interface IBlobStorageService
{
    public Task<string> UploadToBlobAsync(Stream file , string Filename);
}
