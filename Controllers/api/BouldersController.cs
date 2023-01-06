using AutoMapper;
using MeterReaderCMS.Infrastructure;
using MeterReaderCMS.Models.ViewModels;
using MeterReaderCMS.Models.ViewModels.MeterReader;
using MeterReaderCMS.Repositories.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MeterReaderCMS.Controllers.api
{
    public class BouldersController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IMeterReaderRepository _meterReaderRepository;

        public BouldersController(IMeterReaderRepository meterReaderRepository)
        {
            _meterReaderRepository = meterReaderRepository;
        }

        [CacheFilter(TimeDuration = 100)]
        public IHttpActionResult GetData(string sEcho, string sSearch, int iDisplayStart, int iDisplayLength, string iSortCol_0, string sSortDir_0)
        {
            try
            {
                int iSortCol = Convert.ToInt32(iSortCol_0);
                string sortOrder = sSortDir_0;
                var Count = 0;

                var metersReaders = new List<MeterReaderGridVM>();

                if (!string.IsNullOrEmpty(sSearch))
                {
                    if (MemoryCacher.GetValue(Constant.BoulderList) != null)
                    {
                        metersReaders = (List<MeterReaderGridVM>)MemoryCacher.GetValue(Constant.BoulderList);
                    }
                    else
                    {
                        metersReaders = Mapper.Map<List<MeterReaderGridVM>>(_meterReaderRepository.GetAll().Where(x => x.User.Username == User.Identity.Name)).ToList();
                    }

                    var filterdMetersReaders = metersReaders
                           .Where(m => m.Called.ToString().Contains(sSearch)
                           || m.Date.ToString("dd/MM/yyyy").Contains(sSearch)
                           || m.NoteBookNumber.ToString().Contains(sSearch)
                           || m.UnCalled.ToString().Contains(sSearch)).ToList();

                    MemoryCacher.Add(Constant.BoulderList, filterdMetersReaders, DateTimeOffset.Now.AddMinutes(Constant.CacheTime));

                    Count = filterdMetersReaders.Count();
                    metersReaders = SortFunction(iSortCol, sortOrder, filterdMetersReaders).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }
                else
                {
                    if (MemoryCacher.GetValue(Constant.BoulderList) != null)
                    {
                        metersReaders = (List<MeterReaderGridVM>)MemoryCacher.GetValue(Constant.BoulderList);
                    }
                    else
                    {
                        metersReaders = Mapper.Map<List<MeterReaderGridVM>>(_meterReaderRepository.GetAll().Where(x=>x.User.Username == User.Identity.Name).OrderByDescending(m => m.Date).ToList());
                        MemoryCacher.Add(Constant.BoulderList, metersReaders, DateTimeOffset.Now.AddMinutes(Constant.CacheTime));
                    }

                    Count = metersReaders.Count();
                    metersReaders = SortFunction(iSortCol, sortOrder, metersReaders).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }


                //var called = _meterReaderRepository.GetAll().Sum(x => x.Called);
                //var uncalled = _meterReaderRepository.GetAll().Sum(x => x.UnCalled);
                var bouldersPaged = new SysDataTablePager<MeterReaderGridVM>(metersReaders, Count, Count, sEcho);

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

        private List<MeterReaderGridVM> SortFunction(int iSortCol, string sortOrder, List<MeterReaderGridVM> list)
        {
            if (iSortCol ==2 || iSortCol == 3 || iSortCol == 4 || iSortCol == 5 || iSortCol == 6 || iSortCol == 7)
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
                            list = list.OrderByDescending(c => c.UnCalled).ToList();
                        else
                            list = list.OrderBy(c => c.UnCalled).ToList();
                        break;
                    case 4:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.Called).ToList();
                        else
                            list = list.OrderBy(c => c.Called).ToList();
                        break;
                    case 5:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.NoteBookNumber).ToList();
                        else
                            list = list.OrderBy(c => c.NoteBookNumber).ToList();
                        break;
                    case 6:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.Date).ToList();
                        else
                            list = list.OrderBy(c => c.Date).ToList();
                        break;
                    case 7:
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
