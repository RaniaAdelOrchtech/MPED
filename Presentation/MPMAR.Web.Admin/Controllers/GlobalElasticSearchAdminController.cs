using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business.Interfaces;
using MPMAR.Business.Models;
using MPMAR.Data;

namespace MPMAR.Web.Admin.Controllers
{
    [Authorize]
    public class GlobalElasticSearchAdminController : Controller
    {
        private readonly IGlobalElasticSearchService _globalElasticSearchService;
        private readonly IPageRouteRepository _pageRouteRepository;
        private readonly IPhotoArchiveElasticSearchService _photoArchiveElasticSearchService;
        private readonly IPageNewsElasticSearchService _pageNewsElasticSearchService;
        private readonly IPageNewsRepository _pageNewsRepository;
        private readonly ApplicationDbContext _dataAccessService;

        public GlobalElasticSearchAdminController(ApplicationDbContext dataAccessService, IGlobalElasticSearchService globalElasticSearchService, IPageRouteRepository pageRouteRepository, IPhotoArchiveElasticSearchService photoArchiveElasticSearchService, IPageNewsElasticSearchService pageNewsElasticSearchService, IPageNewsRepository PageNewsRepository)
        {
            _globalElasticSearchService = globalElasticSearchService;
            _pageRouteRepository = pageRouteRepository;
            _photoArchiveElasticSearchService = photoArchiveElasticSearchService;
            _pageNewsElasticSearchService = pageNewsElasticSearchService;
            _pageNewsRepository = PageNewsRepository;
            _dataAccessService = dataAccessService;
        }
        [HttpGet("add-global-data-to-elasticsearch")]
        public IActionResult AddGlobalDataToElasticSearch()
        {
            try
            {
                var data = new List<GlobalSearchModel>();
                data.AddRange(GetPagesData());

                _globalElasticSearchService.AddManyAsync(data.ToArray());
                return Ok("data inserted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("add-photo-archive-data-to-elasticsearch")]
        public IActionResult AddPhotoArchiveDataToElasticSearch()
        {
            try
            {

                var items = _dataAccessService.PhotoArchive.Where(i => i.IsDeleted != true && i.IsActive == true).OrderBy(i => i.Order).ToArray();
                foreach (var item in items)
                {
                    item.EnPhotoArchiveType = item.EnPhotoArchiveType?.Trim();
                }
                _photoArchiveElasticSearchService.AddManyAsync(items);
                return Ok("data inserted");
            }
            catch (Exception ex)
            {
                return BadRequest("can't insert data");
            }
        }


        [HttpGet("add-page-news-data-to-elasticsearch")]
        public IActionResult AddPageNewsDataToElasticSearch()
        {
            try
            {
                _pageNewsElasticSearchService.AddPageNewsDataToElasticSearch(_pageNewsRepository.GetPageNews().ToArray());
                return Ok("data inserted");
            }
            catch
            {
                return BadRequest("can't insert data");
            }
        }

        [HttpGet("add-all-data-to-elasticsearch")]
        public IActionResult AddAllDataToElasticSearch()
        {
            try
            {
                AddGlobalDataToElasticSearch();
                AddPhotoArchiveDataToElasticSearch();
                AddPageNewsDataToElasticSearch();

                return Ok("data inserted");
            }
            catch
            {
                return BadRequest("can't insert data");
            }
        }
        [NonAction]
        private List<GlobalSearchModel> GetPagesData()
        {

            var data = new List<GlobalSearchModel>();
            var pageRouteIds = _pageRouteRepository.GetAllId();
            foreach (var pageRouteId in pageRouteIds)
            {
                data.Add(_pageRouteRepository.GetPageData(pageRouteId));
            }
            return data;
        }


        [HttpGet("remove-all-data-from-elasticsearch")]
        public IActionResult RemoveAllDataToElasticSearch()
        {
            try
            {
                _globalElasticSearchService.DeleteAllAsync();

                return Ok("data deleted");
            }
            catch
            {
                return BadRequest("can't delete data");
            }
        }
    }
}
