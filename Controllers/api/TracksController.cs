using AutoMapper;
using MeterReaderCMS.Helper;
using MeterReaderCMS.Infrastructure;
using MeterReaderCMS.Models.DTO;
using MeterReaderCMS.Repositories.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace MeterReaderCMS.Controllers.api
{
    public class TracksController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ITrackRepository _trackRepository;

        public TracksController(ITrackRepository trackRepository)
        {
            this._trackRepository = trackRepository;
        }

        [CacheFilter(TimeDuration = 100)]
        public IHttpActionResult GetData(string sEcho, string sSearch, int iDisplayStart, int iDisplayLength, string iSortCol_0, string sSortDir_0)
        {
            try
            {
                int iSortCol = Convert.ToInt32(iSortCol_0);
                string sortOrder = sSortDir_0;
                var Count = 0;

                var tracks = new List<TrackListItemDTO>();

                if (!string.IsNullOrEmpty(sSearch))
                {
                    if (MemoryCacher.GetValue(Constant.TrackList) != null)
                    {
                        tracks = (List<TrackListItemDTO>)MemoryCacher.GetValue(Constant.TrackList);
                    }
                    else
                    {
                        tracks = Mapper.Map<List<TrackListItemDTO>>(_trackRepository.GetAll().Where(x => x.User.Username == User.Identity.Name)).ToList();
                    }

                    var filterdTracks = tracks
                           .Where(m => m.Called.ToString().Contains(sSearch)
                           || m.Date.ToString("dd/MM/yyyy").Contains(sSearch)
                           || m.NoteBookNumber.ToString().Contains(sSearch)
                           || m.UnCalled.ToString().Contains(sSearch)).ToList();

                    MemoryCacher.Add(Constant.TrackList, filterdTracks, DateTimeOffset.Now.AddMinutes(Constant.CacheTime));

                    Count = filterdTracks.Count();
                    tracks = SortFunction(iSortCol, sortOrder, filterdTracks).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }
                else
                {
                    if (MemoryCacher.GetValue(Constant.TrackList) != null)
                    {
                        tracks = (List<TrackListItemDTO>)MemoryCacher.GetValue(Constant.TrackList);
                    }
                    else
                    {
                        tracks = Mapper.Map<List<TrackListItemDTO>>(_trackRepository.GetAll().Where(x => x.User.Username == User.Identity.Name).ToList());
                        MemoryCacher.Add(Constant.TrackList, tracks, DateTimeOffset.Now.AddMinutes(Constant.CacheTime));
                    }

                    Count = tracks.Count();
                    tracks = SortFunction(iSortCol, sortOrder, tracks).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }

                var tracksPaged = new SysDataTablePager<TrackListItemDTO>(tracks, Count, Count, sEcho);

                return Ok(tracksPaged);
            }
            catch (Exception ex)
            {
                logger.Error($"GetData() {DateTime.Now}");
                logger.Error(ex);
                logger.Error("==============================");
                return InternalServerError();
            }
        }

        private List<TrackListItemDTO> SortFunction(int iSortCol, string sortOrder, List<TrackListItemDTO> list)
        {
            if (iSortCol == 2 || iSortCol == 3 || iSortCol == 4 || iSortCol == 5 || iSortCol == 6 || iSortCol == 7)
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
