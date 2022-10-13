using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;

namespace IRFestival.Api.Domain
{
    public class Article
    {

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "tag")]
        public string Tag { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }


        [JsonProperty(PropertyName = "subtitle")]
        public string Subtitle { get; set; }        

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }


        [JsonProperty(PropertyName = "imagePath")]
        public string ImagePath { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
    }

    public enum Status
    {
        Published,
        Unpublished
    }
}