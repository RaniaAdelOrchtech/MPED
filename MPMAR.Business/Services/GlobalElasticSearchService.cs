using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MPMAR.Business.Interfaces;
using MPMAR.Business.Models;
using MPMAR.Data;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPMAR.Business.Services
{
    public class GlobalElasticSearchService : IGlobalElasticSearchService
    {
        private readonly IElasticClient _elasticClient;

        private readonly ILogger _logger;
        private string indexGlobalSearch;
        private string indexPhotoArchive;
        private string indexPageNews;
        public GlobalElasticSearchService(IElasticClient elasticClient, ILogger<GlobalSearchModel> logger, IConfiguration configuration)
        {
            _elasticClient = elasticClient;
            _logger = logger;
            indexGlobalSearch = configuration["elasticsearch:indexGlobalSearch"];
            indexPhotoArchive = configuration["elasticsearch:indexPhotoArchive"];
            indexPageNews = configuration["elasticsearch:indexPageNews"];
        }

        public async Task AddAsync(GlobalSearchModel globalSearch)
        {
            var result = await _elasticClient.IndexAsync(globalSearch, i => i
    .Index(indexGlobalSearch).Id(globalSearch.Id).Refresh(Elasticsearch.Net.Refresh.True));
        }

        public async Task AddManyAsync(GlobalSearchModel[] globalSearch)
        {
            _elasticClient.DeleteByQuery<GlobalSearchModel>(del => del
.Index(indexGlobalSearch).Query(q => q.QueryString(qs => qs.Query("*")))
);
            var result = await _elasticClient.IndexManyAsync(globalSearch, indexGlobalSearch);
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

        public async Task DeleteAsync(int pageRouteId)
        {
            await _elasticClient.DeleteAsync<GlobalSearchModel>(pageRouteId, d => d.Index(indexGlobalSearch));
        }

        public async Task<SearchViewModel> FindAsync(string query, int page = 1, int pageSize = 10)
        {
            var SearchViewModel = new SearchViewModel();

            query = string.IsNullOrWhiteSpace(query) ? "" : query;

            var count = await _elasticClient.CountAsync<GlobalSearchModel>(c => c
      .Index(indexGlobalSearch).Query(q => q.QueryString(d => d.Query('*' + query + '*'
      ))));


            var pageNameResponse = await _elasticClient.SearchAsync<GlobalSearchModel>(
                   s => s.Index(indexGlobalSearch).Query(q => q.Bool(b => b
        .Should(mu => mu
            .Match(m => m
                .Field(f => f.EnTitle)
                .Query(query)
            )
        )) || q.Bool(b => b
         .Should(mu => mu
             .Match(m => m
                 .Field(f => f.ArTitle)
                 .Query(query)
             )
         ))).From((page - 1) * pageSize)
                       .Size(pageSize));

            var pageNameResponseList = pageNameResponse.Documents.ToList();
            var pageNameResponseListCount = pageSize - pageNameResponseList.Count;


            var allResponse = await _elasticClient.SearchAsync<GlobalSearchModel>(
                   s => s.Index(indexGlobalSearch).Query(q => q.QueryString(d => d.Query('*' + query + '*')))
                       .From((page - 1) * pageNameResponseListCount)
                       .Size(pageNameResponseListCount));

            pageNameResponseList.ForEach(d => d.Order = 1);
            var allResponseList = allResponse.Documents.ToList();
            allResponseList.RemoveAll(x => pageNameResponseList.Select(d => d.Id).Contains(x.Id));
            allResponseList.ForEach(d => d.Order = 2);
            var data = pageNameResponseList.Union(allResponseList).OrderBy(d => d.Order).ToList();


            if (!allResponse.IsValid || !pageNameResponse.IsValid)
            {
                // We could handle errors here by checking response.OriginalException 
                //or response.ServerError properties
                _logger.LogError("Failed to search documents");
                return new SearchViewModel { };
            }

            SearchViewModel.GlobalSearchModels = data;
            SearchViewModel.Count = count.Count;
            return SearchViewModel;
        }

        public async Task DeleteAllAsync()
        {


            await _elasticClient.DeleteByQueryAsync<GlobalSearchModel>(del => del
             .Index(indexGlobalSearch).Query(q => q.QueryString(qs => qs.Query("*"))));

            await _elasticClient.DeleteByQueryAsync<PhotoArchive>(del => del
    .Index(indexPhotoArchive).Query(q => q.QueryString(qs => qs.Query("*"))));

            await _elasticClient.DeleteByQueryAsync<PageNews>(del => del
    .Index(indexPageNews).Query(q => q.QueryString(qs => qs.Query("*"))));
        }
    }
}
