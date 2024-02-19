using Azure.Storage;
using Azure.Storage.Blobs;
using System.Reflection.Metadata;
using TestTaskForInternshipCamp.Server;

namespace learningBlob
{
    public class BlobStorageService
    {
        private readonly string _storageAccount = "testtaskforinternshipcam";
        private readonly string _key = "sdDymC/ShzZxKKhOyjRiD/PWb5CFGGrdybZvKOHU9XM5SYSiHJRJpTW7gBuQd688I29Pak5QAYkC+ASt2itj2g==";
        private readonly BlobContainerClient _fileContainer;

        public BlobStorageService()
        {
            var credential = new StorageSharedKeyCredential(_storageAccount, _key);

            Uri blobUri = new Uri($"https://{_storageAccount}.blob.core.windows.net/");
            BlobServiceClient blobServiceClient = new BlobServiceClient(blobUri, credential);

            _fileContainer = blobServiceClient.GetBlobContainerClient("file");
        }

        

        public virtual async Task<BlobResponceDto> UpLoadAsync(FileUploadModel model)
        {
            BlobResponceDto response = new();
            BlobClient client = _fileContainer.GetBlobClient(model.File.FileName);

            await using (Stream? data = model.File.OpenReadStream())
            {
                await client.UploadAsync(data);
            }

            client.SetMetadata(new Dictionary<string, string>
            {
                {"email", model.Email}
             });

            response.status = $"File {model.File.FileName} uploaded Successfully";
            response.Error = false;
            response.Blob.Uri = client.Uri.AbsoluteUri;
            response.Blob.Name = client.Name;

            return response;
        }        
    }
}
