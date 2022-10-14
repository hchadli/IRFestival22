using System.Security.AccessControl;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using IRFestival.Api.Options;
using Microsoft.Extensions.Options;

namespace IRFestival.Api.Common
{
    public class BlobUtility
    {
        private StorageSharedKeyCredential Credential { get; }
        private BlobServiceClient Client { get; }
        private BlobSettingsOptions Options { get; }

        public BlobUtility(StorageSharedKeyCredential credential, BlobServiceClient client, IOptions<BlobSettingsOptions> options)
        {
            Credential = credential;
            Client = client;
            Options = options.Value;
        }

        public BlobContainerClient GetPicturesContainer() => Client.GetBlobContainerClient(Options.PicturesContainer);


        public string GetSasUri(BlobContainerClient container, string blobName)
        {
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = container.Name,
                BlobName = blobName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow.AddDays(-1),
                ExpiresOn = DateTimeOffset.UtcNow.AddDays(2),
            };

            sasBuilder.SetPermissions(BlobContainerSasPermissions.All);

            string sasToken = sasBuilder.ToSasQueryParameters(Credential).ToString();

            return $"{container.Uri.AbsoluteUri}/{blobName}?{sasToken}";
        }
    }
}
