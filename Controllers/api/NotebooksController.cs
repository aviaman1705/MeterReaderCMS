using NLog;
using System;
using AutoMapper;
using System.Linq;
using System.Web.Http;
using MeterReaderCMS.Helper;
using System.Collections.Generic;
using MeterReaderCMS.Infrastructure;
using MeterReaderCMS.Models.DTO.Notebook;
using MeterReaderCMS.Repositories.Interfaces;
using System.Web.Http.Cors;
using MeterReaderCMS.Models.DTO;

namespace MeterReaderCMS.Controllers.api
{
    public class NotebooksController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private INotebookRepository _notebookRepository;

        public NotebooksController(INotebookRepository notebookRepository)
        {
            _notebookRepository = notebookRepository;
        }


        [HttpGet]
        public IHttpActionResult LoadData(PaginationDTO paginationDTO)
        {
            try
            {
                var queryable = _notebookRepository.GetAll().Take(10).ToList();
                return Ok(queryable);
                //await HttpContext.InsertParametersPagintionInHelper(queryable);
                //var actors = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
                //return mapper.Map<List<ActorDTO>>(actors);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [CacheFilter(TimeDuration = 100)]
        public IHttpActionResult GetData(string sEcho, string sSearch, int iDisplayStart, int iDisplayLength, string iSortCol_0, string sSortDir_0)
        {
            try
            {
                int iSortCol = Convert.ToInt32(iSortCol_0);
                string sortOrder = sSortDir_0;
                var Count = 0;

                var notebooks = new List<NotebookDTO>();

                if (!string.IsNullOrEmpty(sSearch))
                {
                    if (MemoryCacher.GetValue(Constant.NotebookList) != null)
                    {
                        notebooks = (List<NotebookDTO>)MemoryCacher.GetValue(Constant.NotebookList);
                    }
                    else
                    {
                        notebooks = Mapper.Map<List<NotebookDTO>>(_notebookRepository.GetAll()).ToList();
                    }

                    var filterdNotebooks = notebooks
                           .Where(m => m.Number.ToString().Contains(sSearch) || m.StreetName.Contains(sSearch)).ToList();

                    MemoryCacher.Add(Constant.NotebookList, filterdNotebooks, DateTimeOffset.Now.AddMinutes(Constant.CacheTime));

                    Count = filterdNotebooks.Count();
                    notebooks = SortFunction(iSortCol, sortOrder, filterdNotebooks).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }
                else
                {
                    if (MemoryCacher.GetValue(Constant.NotebookList) != null)
                    {
                        notebooks = (List<NotebookDTO>)MemoryCacher.GetValue(Constant.NotebookList);
                    }
                    else
                    {
                        notebooks = Mapper.Map<List<NotebookDTO>>(_notebookRepository.GetAll().OrderBy(m => m.Number).ToList());
                        MemoryCacher.Add(Constant.NotebookList, notebooks, DateTimeOffset.Now.AddMinutes(Constant.CacheTime));
                    }

                    Count = notebooks.Count();
                    notebooks = SortFunction(iSortCol, sortOrder, notebooks).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }

                var notebooksPaged = new SysDataTablePager<NotebookDTO>(notebooks, Count, Count, sEcho);

                return Ok(notebooksPaged);
            }
            catch (Exception ex)
            {
                logger.Error($"GetData() {DateTime.Now}");
                logger.Error(ex);
                logger.Error("==============================");
                return InternalServerError();
            }
        }

        private List<NotebookDTO> SortFunction(int iSortCol, string sortOrder, List<NotebookDTO> list)
        {
            if (iSortCol == 2 || iSortCol == 3 || iSortCol == 4)
            {
                switch (iSortCol)
                {
                    case 2:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.StreetName).ToList();
                        else
                            list = list.OrderBy(c => c.StreetName).ToList();
                        break;
                    case 3:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.Number).ToList();
                        else
                            list = list.OrderBy(c => c.Number).ToList();
                        break;
                    case 4:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.Id).ToList();
                        else
                            list = list.OrderBy(c => c.Id).ToList();
                        break;
                }
            }

            return list;
        }
    }
}
