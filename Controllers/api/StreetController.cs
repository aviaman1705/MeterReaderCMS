using AutoMapper;
using MeterReaderCMS.Helper;
using MeterReaderCMS.Infrastructure;
using MeterReaderCMS.Models.DTO.Street;
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
    public class StreetController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IStreetRepository _streetRepository;

        public StreetController(IStreetRepository streetRepository)
        {
            _streetRepository = streetRepository;
        }

        [CacheFilter(TimeDuration = 100)]
        public IHttpActionResult GetData(string sEcho, string sSearch, int iDisplayStart, int iDisplayLength, string iSortCol_0, string sSortDir_0)
        {
            try
            {
                int iSortCol = Convert.ToInt32(iSortCol_0);
                string sortOrder = sSortDir_0;
                var Count = 0;

                var streets = new List<StreetDTO>();

                if (!string.IsNullOrEmpty(sSearch))
                {
                    if (MemoryCacher.GetValue(Constant.StreetList) != null)
                    {
                        streets = (List<StreetDTO>)MemoryCacher.GetValue(Constant.StreetList);
                    }
                    else
                    {
                        streets = Mapper.Map<List<StreetDTO>>(_streetRepository.GetAll()).ToList();
                    }

                    var filterdStreets = streets
                           .Where(m => m.Title.ToString().Contains(sSearch)).ToList();

                    MemoryCacher.Add(Constant.StreetList, filterdStreets, DateTimeOffset.Now.AddMinutes(Constant.CacheTime));

                    Count = filterdStreets.Count();
                    streets = SortFunction(iSortCol, sortOrder, filterdStreets).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }
                else
                {
                    if (MemoryCacher.GetValue(Constant.TrackList) != null)
                    {
                        streets = (List<StreetDTO>)MemoryCacher.GetValue(Constant.StreetList);
                    }
                    else
                    {
                        streets = Mapper.Map<List<StreetDTO>>(_streetRepository.GetAll().ToList());
                        MemoryCacher.Add(Constant.StreetList, streets, DateTimeOffset.Now.AddMinutes(Constant.CacheTime));
                    }

                    Count = streets.Count();
                    streets = SortFunction(iSortCol, sortOrder, streets).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }

                var streetsPaged = new SysDataTablePager<StreetDTO>(streets, Count, Count, sEcho);

                return Ok(streetsPaged);
            }
            catch (Exception ex)
            {
                logger.Error($"GetData() {DateTime.Now}");
                logger.Error(ex);
                logger.Error("==============================");
                return InternalServerError(ex);
            }
        }

        private List<StreetDTO> SortFunction(int iSortCol, string sortOrder, List<StreetDTO> list)
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
                            list = list.OrderByDescending(c => c.Title).ToList();
                        else
                            list = list.OrderBy(c => c.Title).ToList();
                        break;
                }
            }

            return list;
        }
    }
}
