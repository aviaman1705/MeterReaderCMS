using AutoMapper;
using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Repositories.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace MeterReaderCMS.Controllers.api
{
    public class SearchController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ISearchRepository _searchRepository;     

        public SearchController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;            
        }

        public IHttpActionResult GetSearchResult(string term)
        {
            try
            {
                List<SearchItem> searchResult = _searchRepository.GetSearchResult(term).ToList();
                return Ok(searchResult);
            }
            catch (Exception ex)
            {
                logger.Error($"GetSearchResult() {DateTime.Now}");
                logger.Error(ex);
                logger.Error("==============================");
                return InternalServerError();
            }
        }

        [Route("api/search/searchItem/{item}")]
        [HttpGet]
        public IHttpActionResult GetSearchItem(string item)
        {
            try
            {
                //int number;
                //if (int.TryParse(item, out number))
                //{
                //    NotebookDTO itemData = Mapper.Map<NotebookDTO>(_notebookRepository.GetAll().Where(x => x.Number == number).FirstOrDefault());
                //    return Ok(itemData);
                //}
                //else
                //{
                //    return NotFound();
                //}
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.Error($"GetSearchResult() {DateTime.Now}");
                logger.Error(ex);
                logger.Error("==============================");
                return InternalServerError(ex);
            }
        }

        //api/Search/SearchItem
    }
}
