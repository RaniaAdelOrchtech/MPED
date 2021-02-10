using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPMAR.Business.Services
{
    public class PhotoArchiveElasticSearchService : IPhotoArchiveElasticSearchService
    {
        private readonly IElasticClient _elasticClient;
        private string index;
        private readonly ILogger _logger;
        public PhotoArchiveElasticSearchService(IElasticClient elasticClient, ILogger<PhotoArchive> logger, IConfiguration configuration)
        {
            _elasticClient = elasticClient;
            _logger = logger;
            index = configuration["elasticsearch:indexPhotoArchive"];
        }

        public async Task AddAsync(PhotoArchive photoArchive)
        {
            var result = await _elasticClient.IndexAsync(photoArchive, i => i
       .Index(index).Id(photoArchive.Id).Refresh(Elasticsearch.Net.Refresh.True));

        }

        public async Task AddManyAsync(PhotoArchive[] photoArchive)
        {
            _elasticClient.DeleteByQuery<PhotoArchive>(del => del
    .Index(index).Query(q => q.QueryString(qs => qs.Query("*")))
);

            var result = await _elasticClient.IndexManyAsync(photoArchive, index);

            if (result.Errors)
            {
                // the response can be inspected for errors
                foreach (var itemWithError in result.ItemsWithErrors)
                {
                    _logger.LogError("Failed to index document {0}: {1}",
                        itemWithError.Id, itemWithError.Error);
                }
                throw new Exception();
            }

        }



        public async Task DeleteAsync(PhotoArchive photoArchive)
        {
            await _elasticClient.DeleteAsync<PhotoArchive>(photoArchive.Id, d => d.Index(index));
        }

        public async Task<IReadOnlyCollection<PhotoArchive>> Find(string query, string archType, int page = 1, int pageSize = 50)
        {
            ISearchResponse<PhotoArchive> response;
            if (archType == "كل")
            {
                response = await _elasticClient.SearchAsync<PhotoArchive>(
                s => s.Index(index).Query(q => q.QueryString(d => d.Query('*' + query + '*')))
                    .From((page - 1) * pageSize)
                    .Size(pageSize));
            }
            else
            {
                response = await _elasticClient.SearchAsync<PhotoArchive>(
           s => s.Index(index).Query(q => q.QueryString(d => d.Query('*' + query + '*')) &&
           q.Bool(b => b
            .Must(mu => mu
                .Match(m => m
                    .Field(f => f.ArPhotoArchiveType)
                    .Query(archType)
                )
            ))).From((page - 1) * pageSize).Size(pageSize));
            }



            if (!response.IsValid)
            {
                // We could handle errors here by checking response.OriginalException 
                //or response.ServerError properties
                _logger.LogError("Failed to search documents");
                return new PhotoArchive[] { };
            }
            return response.Documents;
        }
    }
}
