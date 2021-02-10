using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPMAR.Business.Services
{
    public class PageNewsElasticSearchService : IPageNewsElasticSearchService
    {
        private readonly IElasticClient _elasticClient;

        private readonly ILogger _logger;
        private string index;
        public PageNewsElasticSearchService(IElasticClient elasticClient, ILogger<PageNewsElasticSearchService> logger, IConfiguration configuration)
        {
            _elasticClient = elasticClient;
            _logger = logger;
            index = configuration["elasticsearch:indexPageNews"];

        }
        public async Task AddAsync(PageNews pageNews)
        {
            var result = await _elasticClient.IndexAsync(pageNews, i => i
       .Index(index).Id(pageNews.Id).Refresh(Elasticsearch.Net.Refresh.True));

        }

        public async Task AddManyAsync(PageNews[] pageNews)

        {
            _elasticClient.DeleteByQuery<PageNews>(del => del
.Index(index).Query(q => q.QueryString(qs => qs.Query("*")))
);
            var result = await _elasticClient.IndexManyAsync(pageNews, index);
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

        public async Task DeleteAsync(PageNews pageNews)
        {
            await _elasticClient.DeleteAsync<PageNews>(pageNews.Id, d => d.Index(index));
        }

        public async Task<IReadOnlyCollection<PageNews>> Find(string query, int newsTypeId, string lang, int page = 1, int pageSize = 10)
        {
            ISearchResponse<PageNews> response;

            if (lang == "en")

            {
                if (newsTypeId == 0)
                {
                    response = await _elasticClient.SearchAsync<PageNews>(
                    s => s.Index(index).Query(q => q.QueryString(d => d.Query('*' + query + '*')) &&
                   q.Bool(b => b
                       .Must(m => m
                            .Exists(e => e
                                .Field(f => f.EnShortDescription)
                           )
                       ))
              &&
           q.Bool(b => b
                       .Must(m => m
                            .Exists(e => e
                                .Field(f => f.EnTitle)
                           )
                       ))).Sort(q=>q.Descending(u=>u.Date))
                        .From((page - 1) * pageSize)
                        .Size(pageSize));
                }
                else
                {
                    response = await _elasticClient.SearchAsync<PageNews>(
               s => s.Index(index).Query(q => q.QueryString(d => d.Query('*' + query + '*'))
               &&
               q.Nested(n => n
                    .Path(p => p.NewsTypesForNews)
                    .Query(qq => qq.Match(m => m.Field(f => f.NewsTypesForNews.First().NewsTypeId).Query("" + newsTypeId))))
                      &&
           q.Bool(b => b
                       .Must(m => m
                            .Exists(e => e
                                .Field(f => f.EnShortDescription)
                           )
                       ))
              &&
           q.Bool(b => b
                       .Must(m => m
                            .Exists(e => e
                                .Field(f => f.EnTitle)
                           )
                       ))
               ).Sort(q => q.Descending(u => u.Date)).From((page - 1) * pageSize).Size(pageSize));
                }

            }
            else
            {
                if (newsTypeId == 0)
                {
                    response = await _elasticClient.SearchAsync<PageNews>(
                    s => s.Index(index).Query(q => q.QueryString(d => d.Query('*' + query + '*'))).Sort(q => q.Descending(u => u.Date))
                        .From((page - 1) * pageSize)
                        .Size(pageSize));
                }
                else
                {
                    response = await _elasticClient.SearchAsync<PageNews>(
               s => s.Index(index).Query(q => q.QueryString(d => d.Query('*' + query + '*'))
               &&
               q.Nested(n => n
                    .Path(p => p.NewsTypesForNews)
                    .Query(qq => qq.Match(m => m.Field(f => f.NewsTypesForNews.First().NewsTypeId).Query("" + newsTypeId))))
               ).Sort(q => q.Descending(u => u.Date)).From((page - 1) * pageSize).Size(pageSize));
                }
            }



            if (!response.IsValid)
            {
                // We could handle errors here by checking response.OriginalException 
                //or response.ServerError properties
                _logger.LogError("Failed to search documents");
                return new PageNews[] { };
            }
            return response.Documents;
        }

        public async Task AddPageNewsDataToElasticSearch(PageNews[] news)
        {
            await AddManyAsync(news.ToArray());
        }

        public async Task<long> GetCount(string query, int newsTypeId, string lang)
        {
            CountResponse response;
            if (lang == "en")
            {
                if (newsTypeId == 0)
                {
                    response = await _elasticClient.CountAsync<PageNews>(
                    s => s.Index(index).Query(q => q.QueryString(d => d.Query('*' + query + '*')) &&
                   q.Bool(b => b
                       .Must(m => m
                            .Exists(e => e
                                .Field(f => f.EnShortDescription)
                           )
                       ))
              &&
           q.Bool(b => b
                       .Must(m => m
                            .Exists(e => e
                                .Field(f => f.EnTitle)
                           )
                       ))));
                }
                else
                {
                    response = await _elasticClient.CountAsync<PageNews>(
               s => s.Index(index).Query(q => q.QueryString(d => d.Query('*' + query + '*'))
               &&
               q.Nested(n => n
                    .Path(p => p.NewsTypesForNews)
                    .Query(qq => qq.Match(m => m.Field(f => f.NewsTypesForNews.First().NewsTypeId).Query("" + newsTypeId)))) 
               
               &&
                   q.Bool(b => b
                       .Must(m => m
                            .Exists(e => e
                                .Field(f => f.EnShortDescription)
                           )
                       ))
              &&
           q.Bool(b => b
                       .Must(m => m
                            .Exists(e => e
                                .Field(f => f.EnTitle)
                           )
                       ))
               ));
                }
            }
            else
            {
                if (newsTypeId == 0)
                {
                    response = await _elasticClient.CountAsync<PageNews>(
                    s => s.Index(index).Query(q => q.QueryString(d => d.Query('*' + query + '*'))));
                }
                else
                {
                    response = await _elasticClient.CountAsync<PageNews>(
               s => s.Index(index).Query(q => q.QueryString(d => d.Query('*' + query + '*'))
               &&
               q.Nested(n => n
                    .Path(p => p.NewsTypesForNews)
                    .Query(qq => qq.Match(m => m.Field(f => f.NewsTypesForNews.First().NewsTypeId).Query("" + newsTypeId))))
               ));
                }
            }



            if (!response.IsValid)
            {
                // We could handle errors here by checking response.OriginalException 
                //or response.ServerError properties
                _logger.LogError("Failed to search documents");
                return 0;
            }
            return response.Count;
        }
    }
}
