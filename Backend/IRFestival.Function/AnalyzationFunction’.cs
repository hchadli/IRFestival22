using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace IRFestival.Function
{
    public class AnalyzationFunction
    {
        [FunctionName("AnalyzationFunction")]
        public void Run([BlobTrigger("festivalpics-uploaded/{name}", Connection = "BlobStorageConnection")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
