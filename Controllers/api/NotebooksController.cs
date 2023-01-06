using AutoMapper;
using MeterReaderCMS.Infrastructure;
using MeterReaderCMS.Models.ViewModels;
using MeterReaderCMS.Models.ViewModels.MeterReader;
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
    public class NotebooksController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private INotebookRepository _notebookRepository;

        public NotebooksController(INotebookRepository notebookRepository)
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

                var notebooks = new List<NotebookVM>();

                if (!string.IsNullOrEmpty(sSearch))
                {
                    if (MemoryCacher.GetValue(Constant.NotebookList) != null)
                    {
                        notebooks = (List<NotebookVM>)MemoryCacher.GetValue(Constant.NotebookList);
                    }
                    else
                    {
                        notebooks = Mapper.Map<List<NotebookVM>>(_notebookRepository.GetAll()).ToList();
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
                        notebooks = (List<NotebookVM>)MemoryCacher.GetValue(Constant.NotebookList);
                    }
                    else
                    {
                        notebooks = Mapper.Map<List<NotebookVM>>(_notebookRepository.GetAll().OrderBy(m => m.Number).ToList());
                        MemoryCacher.Add(Constant.NotebookList, notebooks, DateTimeOffset.Now.AddMinutes(Constant.CacheTime));
                    }

                    Count = notebooks.Count();
                    notebooks = SortFunction(iSortCol, sortOrder, notebooks).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }

                var bouldersPaged = new SysDataTablePager<NotebookVM>(notebooks, Count, Count, sEcho);

                return Ok(bouldersPaged);
            }
            catch (Exception ex)
            {
                logger.Error($"GetData() {DateTime.Now}");
                logger.Error(ex);
                logger.Error("==============================");
                return InternalServerError();
            }
        }

        private List<NotebookVM> SortFunction(int iSortCol, string sortOrder, List<NotebookVM> list)
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
