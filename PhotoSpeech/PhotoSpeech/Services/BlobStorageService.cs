using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Components.Forms;
using PhotoSpeech.Services.Interfaces;
using System.IO;

namespace PhotoSpeech.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly IConfiguration _configuration;
        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateUrlForRandomImage(string nameOfTheItem)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_configuration["BlobStorage:ConnectionString"]);
            var containers = blobServiceClient.GetBlobContainers();

            if(!containers.Select(o => o.Name).ToList().Contains(nameOfTheItem))
            {
                return string.Empty;
            }

            var container = blobServiceClient.GetBlobContainerClient(nameOfTheItem);
            var blobs = container.GetBlobs().ToList();
            int numberOfBlobs = blobs.Count;

            Random random = new Random();
            int index = random.Next(numberOfBlobs)+1;

            return blobServiceClient.Uri + nameOfTheItem + "/" + blobs[index].Name;
        }

        public async Task SaveFileToBlobStorage(FileStream file, string nameOfTheItem)
        {
            BlobContainerClient container;
            BlobServiceClient blobServiceClient = new BlobServiceClient(_configuration["BlobStorage:ConnectionString"]);
            var containers = blobServiceClient.GetBlobContainers();
            
            if (!containers.Select(o => o.Name).ToList().Contains(nameOfTheItem))
            {
                container = await blobServiceClient.CreateBlobContainerAsync(nameOfTheItem);
            } else
            {
                container = blobServiceClient.GetBlobContainerClient(nameOfTheItem);
            }

            var blobs = container.GetBlobs().ToList();
            var blobName = (blobs.Count+1).ToString() + Path.GetExtension(file.Name);

            var newBlob = container.GetBlobClient(blobName);
            await newBlob.UploadAsync(file);
        }
    }
}
