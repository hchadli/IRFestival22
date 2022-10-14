using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(RFestival.Function.Startup))]
namespace RFestival.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            FunctionsHostBuilderContext context = builder.GetContext();

            builder.Services.AddSingleton(p => new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(context.Configuration.GetValue<string>("ComputerVisionApiKey")))
            {
                Endpoint = context.Configuration.GetValue<string>("ComputerVisionEndpoint")
            });
        }
    }
}
