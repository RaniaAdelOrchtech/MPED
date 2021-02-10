using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MPMAR.Business.Models;
using MPMAR.Data;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Common.Utility
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticsearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var pageNewsIndex = configuration["elasticsearch:indexPageNews"];
            var photoArchiveIndex = configuration["elasticsearch:indexPhotoArchive"];
            var globalSearchIndex = configuration["elasticsearch:indexGlobalSearch"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(pageNewsIndex)
                .DefaultIndex(photoArchiveIndex)
                .DefaultIndex(globalSearchIndex);

            settings.DisableDirectStreaming();
            AddDefaultMappings(settings);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            // DeleteIndex(client, globalSearchIndex);

            CreatePageNewsIndex(client, pageNewsIndex);
            CreatePhotoArchiveIndex(client, photoArchiveIndex);
            CreateGlobalSearchIndex(client, globalSearchIndex);

        }

        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings
                .DefaultMappingFor<PageNews>(m => m
                    .Ignore(p => p.CreationDate)
                    .Ignore(p => p.CreatedById)
                    .Ignore(p => p.CreatedBy)
                    .Ignore(p => p.ApprovalDate)
                    .Ignore(p => p.ApprovedById)
                    .Ignore(p => p.ApprovedBy)
                    .Ignore(p => p.IsActive)
                    .Ignore(p => p.IsDeleted)
                    .Ignore(p => p.ModificationDate)
                    .Ignore(p => p.ModifiedById)
                    .Ignore(p => p.ModifiedBy)
                    .Ignore(p => p.PageRouteId)
                    .Ignore(p => p.PageRoute)
                    .Ignore(p => p.PageNewsVersions)

                );

            settings
                .DefaultMappingFor<PageNewsType>(m => m
                    .Ignore(p => p.IsDeleted)
                    .Ignore(p => p.CreationDate)
                    .Ignore(p => p.CreatedById)
                );

            settings
                .DefaultMappingFor<PhotoArchive>(m => m
                .Ignore(p => p.CreationDate)
                    .Ignore(p => p.CreatedById)
                    .Ignore(p => p.CreatedBy)
                    .Ignore(p => p.ApprovalDate)
                    .Ignore(p => p.ApprovedById)
                    .Ignore(p => p.ApprovedBy)
                    .Ignore(p => p.IsActive)
                    .Ignore(p => p.IsDeleted)
                    .Ignore(p => p.ModificationDate)
                    .Ignore(p => p.ModifiedById)
                    .Ignore(p => p.ModifiedBy)
                    .Ignore(p => p.PageRouteId)
                    .Ignore(p => p.PageRoute)
                    .Ignore(p => p.PhotoArchiveVersions)
                    .Ignore(p => p.PhotosAlbums)
                    .Ignore(p => p.SeoDescriptionAR)
                    .Ignore(p => p.SeoDescriptionEN)
                    .Ignore(p => p.SeoOgTitleAR)
                    .Ignore(p => p.SeoOgTitleEN)
                    .Ignore(p => p.SeoTitleAR)
                    .Ignore(p => p.SeoTitleEN)
                    .Ignore(p => p.SeoTwitterCardAR)
                    .Ignore(p => p.SeoTwitterCardEN)
                );

            settings
             .DefaultMappingFor<GlobalSearchModel>(m => m
                 .Ignore(p => p.Order)
             );


        }
        private static void DeleteIndex(IElasticClient client, string indexName)
        {
            client.Indices.Delete(indexName);
        }
        private static void CreatePageNewsIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName,
                index => index
                .Map<PageNewsType>(x => x.AutoMap())
                .Map<NewsTypesForNews>(x => x.AutoMap())
                .Map<PageNews>(x => x.AutoMap())
            );
        }

        private static void CreatePhotoArchiveIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName,
                index => index
                .Map<PhotoArchive>(x => x.AutoMap())
            );
        }

        private static void CreateGlobalSearchIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName,
                index => index
                .Map<GlobalSearchModel>(x => x.AutoMap())
            );
        }
    }
}
