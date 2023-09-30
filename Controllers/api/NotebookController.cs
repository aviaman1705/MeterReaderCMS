using AutoMapper;
using MeterReaderCMS.Helper;
using MeterReaderCMS.Infrastructure;
using MeterReaderCMS.Models.DTO.Notebook;
using MeterReaderCMS.Repositories.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MeterReaderCMS.Controllers.api
{
    public class NotebookController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private INotebookRepository _notebookRepository;

        public NotebookController(INotebookRepository notebookRepository)
        {
            _notebookRepository = notebookRepository;
        }

        [CacheFilter(TimeDuration = 100)]
        public IHttpActionResult GetData(string sEcho, string sSearch, int iDisplayStart, int iDisplayLength, string iSortCol_0, string sSortDir_0)
        {
            try
            {
                int iSortCol = Convert.ToInt32(iSortCol_0);
                string sortOrder = sSortDir_0;
                var Count = 0;

                var Notebooks = new List<NotebookDTO>();

                if (!string.IsNullOrEmpty(sSearch))
                {
                    if (MemoryCacher.GetValue(Constant.NotebookList) != null)
                    {
                        Notebooks = (List<NotebookDTO>)MemoryCacher.GetValue(Constant.NotebookList);
                    }
                    else
                    {
                        Notebooks = Mapper.Map<List<NotebookDTO>>(_notebookRepository.GetAll()).ToList();
                    }

                    var filterdNotebooks = Notebooks
                           .Where(m => m.Number.ToString().Contains(sSearch)).ToList();

                    MemoryCacher.Add(Constant.NotebookList, filterdNotebooks, DateTimeOffset.Now.AddMinutes(Constant.CacheTime));

                    Count = filterdNotebooks.Count();
                    Notebooks = SortFunction(iSortCol, sortOrder, filterdNotebooks).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }
                else
                {
                    if (MemoryCacher.GetValue(Constant.TrackList) != null)
                    {
                        Notebooks = (List<NotebookDTO>)MemoryCacher.GetValue(Constant.NotebookList);
                    }
                    else
                    {
                        Notebooks = Mapper.Map<List<NotebookDTO>>(_notebookRepository.GetAll().ToList());
                        MemoryCacher.Add(Constant.NotebookList, Notebooks, DateTimeOffset.Now.AddMinutes(Constant.CacheTime));
                    }

                    Count = Notebooks.Count();
                    Notebooks = SortFunction(iSortCol, sortOrder, Notebooks).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }

                var NotebooksPaged = new SysDataTablePager<NotebookDTO>(Notebooks, Count, Count, sEcho);

                return Ok(NotebooksPaged);
            }
            catch (Exception ex)
            {
                logger.Error($"GetData() {DateTime.Now}");
                logger.Error(ex);
                logger.Error("==============================");
                return BadRequest(ex.Message);
            }
        }

        private List<NotebookDTO> SortFunction(int iSortCol, string sortOrder, List<NotebookDTO> list)
        {
            if (iSortCol == 0 || iSortCol == 1)
            {
                switch (iSortCol)
                {
                    case 0:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.Id).ToList();
                        else
                            list = list.OrderBy(c => c.Id).ToList();
                        break;
                    case 1:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.Number).ToList();
                        else
                            list = list.OrderBy(c => c.Number).ToList();
                        break;
                }
            }

            return list;
        }
    }
}
