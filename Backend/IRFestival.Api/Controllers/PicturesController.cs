using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text.Json;
using System.Web;
using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using IRFestival.Api.Common;
using IRFestival.Api.Options;
using System.Text;
using IRFestival.Api.Domain;

namespace IRFestival.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private BlobUtility BlobUtility { get; }
        public PicturesController(BlobUtility blobUtility, IConfiguration configuration)
        {
            BlobUtility = blobUtility;
            _configuration = configuration;
        }

        [HttpGet]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(string[]))]
        public async Task<ActionResult> GetAllPictureUrls()
        {
            var container = BlobUtility.GetPicturesContainer();

            var result = container.GetBlobs()
                .Select(blob => BlobUtility.GetSasUri(container, blob.Name))
                .ToArray();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(AppSettingsOptions))]
        public async Task<ActionResult> PostPicture([FromForm] Mailer mailer)          
        {

            // Post 
            BlobContainerClient container = BlobUtility.GetPicturesContainer();
            var filename = $"{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}{HttpUtility.UrlPathEncode(mailer.File.FileName.Replace(" ", ""))}";
            await container.UploadBlobAsync(filename, mailer.File.OpenReadStream());


            await using (var client =
                         new ServiceBusClient(_configuration.GetConnectionString("ServiceBusSenderConnection")))
            {

                // Create a sender for the queue
                ServiceBusSender sender = client.CreateSender("mails");

                // Create a message that we can send

                var json = JsonSerializer.Serialize(mailer);

                ServiceBusMessage message =
                    new ServiceBusMessage(Encoding.UTF8.GetBytes(json));

                // Send the message
                await sender.SendMessageAsync(message);
            }

            return Ok();
        }
    }
}
