using System.Net;
using IRFestival.Api.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace IRFestival.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        public CosmosClient CosmosClient { get; set; }
        public Container Container { get; set; }


        public ArticlesController(IConfiguration configuration)
        {
            CosmosClient = new CosmosClient(configuration.GetConnectionString("CosmosConnection"));
            Container = CosmosClient.GetContainer("IRFestivalArticles", "WebsiteArticles");
        }

        [HttpPost]
        public async Task<ActionResult> CreateItemAsync([FromBody] Article article)
        {
            await Container.CreateItemAsync(article);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Article))]
        public async Task<IActionResult> GetAsync()
        {
            var database = await CosmosClient.CreateDatabaseIfNotExistsAsync("IRFestivalArticles");
            var containerResponse = await database.Database.CreateContainerIfNotExistsAsync("WebsiteArticles", "/tag");
            var container = containerResponse.Container;
            
            var result = new List<Article>();

            var queryDefinition = container.GetItemLinqQueryable<Article>()
                .Where(p => p.Status == nameof(Status.Published))
                .OrderBy(p => p.Date);

            var iterator = queryDefinition.ToFeedIterator();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                result = response.ToList();
            }

            return Ok(result);
        }


    }
}
