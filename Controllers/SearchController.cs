using AutoMapper;
using MeterReaderCMS.Models.DTO.Search;
using MeterReaderCMS.Repositories.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeterReaderCMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SearchController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ISearchRepository _searchRepository;

        public SearchController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public ActionResult Index(string term)
        {
            try
            {
                List<SearchItemDTO> searchResuls = Mapper.Map<List<SearchItemDTO>>(_searchRepository.GetSearchResult(term)).ToList();
                return View(searchResuls);
            }
            catch (Exception ex)
            {
                logger.Error($"Index() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }
    }
}