using AutoMapper;
using MeterReaderCMS.Helper;
using MeterReaderCMS.Infrastructure;
using MeterReaderCMS.Models.DTO;
using MeterReaderCMS.Models.DTO.Track;
using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Repositories.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeterReaderCMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TracksController : Controller
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private ITrackRepository _trackRepository;
        private INotebookRepository _notebookRepository;
        private IUserRepository _userRepository;

        public TracksController(Logger logger, ITrackRepository trackRepository, INotebookRepository notebookRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _trackRepository = trackRepository;
            _notebookRepository = notebookRepository;
            _userRepository = userRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddTrack()
        {
            try
            {
                ViewBag.NoteBooks = _notebookRepository.LoadNoteBooks();
                return View();
            }
            catch (Exception ex)
            {
                _logger.Error($"AddTrack() {DateTime.Now}");
                _logger.Error(ex.Message);
                _logger.Error("==============================");
                return null;
            }
        }

        [HttpPost]
        public ActionResult AddTrack([Bind(Exclude = "Id")] CreateTrackDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.NoteBooks = _notebookRepository.LoadNoteBooks();
                    return View(model);
                }

                DateTime trackDate = DateTime.ParseExact(model.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (_trackRepository.TrackExistsOnThisDate(trackDate, model.NoteBookID))
                {
                    ModelState.AddModelError("CustomError", "תאריך כבר קיים.");
                    ViewBag.NoteBooks = _notebookRepository.LoadNoteBooks();
                    return View(model);
                }

                Track entity = Mapper.Map<Track>(model);
                User currentUser = _userRepository.GetAll().FirstOrDefault(x => x.Username == User.Identity.Name);

                if (currentUser != null)
                {
                    entity.UserId = currentUser.UserId;
                    _trackRepository.Add(entity);
                    Utilities.updateCache<TrackListItemDTO>(
                        Constant.TrackList,
                        Constant.CacheTime,
                        Mapper.Map<List<TrackListItemDTO>>(_trackRepository.GetAll().OrderByDescending(m => m.Date)));

                    ViewBag.NoteBooks = _notebookRepository.LoadNoteBooks();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.Error($"AddTrack() {DateTime.Now}");
                _logger.Error(ex.Message);
                _logger.Error("==============================");
                return null;
            }
        }
    }
}