using CRM.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CRM.Extensions
{
    public class CompositeFilterDescriptor
    {
        [JsonProperty("logic")]
        public string Logic { get; set; }

        [JsonProperty("filters")]
        public ICollection<CompositeFilterDescriptor> Filters { get; set; }

        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }

        [JsonProperty("ignoreCase")]
        public bool? IgnoreCase { get; set; }
    }

    public class KendoRequest
    {
        public CompositeFilterDescriptor Filters { get; set; }

        public Page Page { get; set; }
    }

    public class KendoResponse<T>
    {
        public IEnumerable<T> Results { get; set; }

        public long Count { get; set; }
    }

    public class Page
    {
        public int Skip { get; set; }

        public int Take { get; set; }
    }

    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(
        this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex)
                .DefaultMappingFor<Customer>(m => m
                    //.Ignore(p => p.IsPublished)
                    .PropertyName(p => p.Id, "id")
                );

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        }
    }
}
