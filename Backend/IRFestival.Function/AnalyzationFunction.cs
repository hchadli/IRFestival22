using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace IRFestival.Function
{
    public class AnalyzationFunction
    {


        public ComputerVisionClient VisionClient { get; }

        private static readonly List<VisualFeatureTypes?> Features = new List<VisualFeatureTypes?>()
        {
            VisualFeatureTypes.Adult
        };
        public AnalyzationFunction(ComputerVisionClient visionClient)
        {
            VisionClient = visionClient;
        }


        [FunctionName("AnalyzationFunction")]
        public async Task Run([BlobTrigger("festivalpics-uploaded/{name}", Connection = "BlobStorageConnection")] byte[] myBlob, string name, ILogger log, Binder binder)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            ImageAnalysis analysis = await VisionClient.AnalyzeImageInStreamAsync(new MemoryStream(myBlob), Features);
            Attribute[] attributes;
            if (analysis.Adult.IsAdultContent || analysis.Adult.IsGoryContent || analysis.Adult.IsRacyContent)
            {
                attributes = new Attribute[]
                {
                    new BlobAttribute($"festivalpics-rejected/{name}",FileAccess.Write),
                    new StorageAccountAttribute("BlobStorageConnection")
                };
            }
            else
            {
                attributes = new Attribute[]
                {
                    new BlobAttribute($"festivalpics-approved/{name}",FileAccess.Write),
                    new StorageAccountAttribute("BlobStorageConnection")
                };
            }

            using Stream FileOutputStream = await binder.BindAsync<Stream>(attributes);
            FileOutputStream.Write(myBlob);
        }
    }
}
