using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Models;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Nest;

namespace Hubtel.eCommerce.Cart.Api.Service
{
    public class ElasticSearchService
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<ElasticSearchService> _logger;

        public ElasticSearchService(IElasticClient elasticClient, ILogger<ElasticSearchService> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }



        public async Task AddAsync(string indexName, object data)
        {
            _logger.LogInformation($"start loading {indexName} in Elastic Search");
            await CheckIndex(indexName);

          var response =  await _elasticClient.BulkAsync(b => b
                .Index(indexName)
                .IndexMany(new List<dynamic> { data }));
            
          Console.WriteLine(response);

            _logger.LogInformation($"finished loading {indexName} in Elastic Search");

        }

        private async Task CheckIndex(string indexName)
        {
            var response = await _elasticClient.Indices.ExistsAsync(indexName);
            if (!response.Exists)
            {
                await _elasticClient.Indices.CreateAsync(indexName);
            }
        }

        public async Task<IReadOnlyCollection<CartViewModel>> GetCartBySearchTerm(string phoneNumber, string itemName, int quantity, DateTime time)
        {
            try
            {
                var query = new List<QueryContainer>
                {
                    new TermQuery {Field = "phoneNumber.keyword", Value = phoneNumber,},

                    new TermQuery {Field = "itemName.keyword", Value = itemName,},

                };

                var timeRange = new DateRangeQuery
                {
                    Field = "addedDate",
                    GreaterThanOrEqualTo = time,
                };
                var quantityRange = new NumericRangeQuery()
                {
                    Field = "quantity",
                    GreaterThanOrEqualTo = quantity,
                };

                query.Add(timeRange);
                query.Add(quantityRange);

                var request = new SearchRequest<Cart>("cart")
                {
                    Query = new BoolQuery { Must = query }
                };

                var response = await _elasticClient.SearchAsync<CartViewModel>(request);

                if (response == null) return new List<CartViewModel>();

                if(!response.IsValid)
                {
                    return new List<CartViewModel>();
                }

                return response.Documents;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"exception occurred while getting cart items: {e}");
                return new List<CartViewModel>();
            }
        }

        public async Task DeleteAsync(string indexName, Guid id, CancellationToken cancellationToken)
        {

            var response = await _elasticClient.DeleteByQueryAsync<CartViewModel>(q => q
                .Query(rq => rq
                    .Match(m => m
                        .Field(f => f.Id)
                        .Query(id.ToString()))
                )
                .Index(indexName), cancellationToken
            );

        }
	}
}
