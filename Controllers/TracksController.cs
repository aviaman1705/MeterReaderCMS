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
            List<ThItem> cells = new List<ThItem>();
            cells.Add(new ThItem() { fieldName = "מחיקה" });
            cells.Add(new ThItem() { fieldName = "עריכה" });
            cells.Add(new ThItem() { fieldName = "רחוב" });
            cells.Add(new ThItem() { fieldName = "מספר פנקס" });
            cells.Add(new ThItem() { fieldName = "מזהה ייחודי" });

            List<TBodyItem> items = _trackRepository.GetAll()
                .Where(x => x.User.Username == User.Identity.Name)
                .Select(x => new TBodyItem() { FieldName = x.NoteBook.StreetName.ToString(), Value = x.NoteBook.StreetName })
                .ToList();


            //  case 2:
            //            if (sortOrder == "desc")
            //    list = list.OrderByDescending(c => c.StreetName).ToList();
            //else
            //    list = list.OrderBy(c => c.StreetName).ToList();
            //break;
            //        case 3:
            //            if (sortOrder == "desc")
            //    list = list.OrderByDescending(c => c.UnCalled).ToList();
            //else
            //    list = list.OrderBy(c => c.UnCalled).ToList();
            //break;
            //        case 4:
            //            if (sortOrder == "desc")
            //    list = list.OrderByDescending(c => c.Called).ToList();
            //else
            //    list = list.OrderBy(c => c.Called).ToList();
            //break;
            //        case 5:
            //            if (sortOrder == "desc")
            //    list = list.OrderByDescending(c => c.NoteBookNumber).ToList();
            //else
            //    list = list.OrderBy(c => c.NoteBookNumber).ToList();
            //break;
            //        case 6:
            //            if (sortOrder == "desc")
            //    list = list.OrderByDescending(c => c.Date).ToList();
            //else
            //    list = list.OrderBy(c => c.Date).ToList();
            //break;
            //        case 7:
            //            if (sortOrder == "desc")
            //    list = list.OrderByDescending(c => c.Id).ToList();
            //else
            //    list = list.OrderBy(c => c.Id).ToList();
            //break;

            return View(cells);
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
                    updateCache();
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

        [HttpGet]
        public ActionResult EditTrack(int id)
        {
            try
            {
                EditTrackDTO model;
                Track dto = _trackRepository.Get(id);

                if (dto == null)
                {
                    return Content("מסלול לא קיים");
                }

                ViewBag.NoteBooks = _notebookRepository.LoadNoteBooks();
                model = Mapper.Map<EditTrackDTO>(dto);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.Error($"EditTrack() {DateTime.Now}");
                _logger.Error(ex.Message);
                _logger.Error("==============================");
                return null;
            }
        }

        [HttpPost]
        public ActionResult EditTrack(EditTrackDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.NoteBooks = _notebookRepository.LoadNoteBooks();
                    return View(model);
                }

                DateTime trackDate = DateTime.ParseExact(model.Date, "dd/MM/yyyy", null);
                Track dto = _trackRepository.Get(model.Id);

                if (dto == null)
                {
                    return Content("המסלול לא קיים");
                }
                else
                {
                    dto = Mapper.Map<Track>(model);
                    dto.Date = trackDate;
                    _trackRepository.Update(dto);
                    updateCache();
                    ViewBag.NoteBooks = _notebookRepository.LoadNoteBooks();

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"EditTrack() {DateTime.Now}");
                _logger.Error(ex.Message);
                _logger.Error("==============================");
                return null;
            }
        }

        [HttpGet]
        public ActionResult DeleteTrack(int id)
        {
            try
            {
                _trackRepository.Delete(id);
                updateCache();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.Error($"DeleteTrack() {DateTime.Now}");
                _logger.Error(ex.Message);
                _logger.Error("==============================");
                return null;
            }
        }

        private void updateCache()
        {
            Utilities.updateCache<TrackListItemDTO>(
                         Constant.TrackList,
                         Constant.CacheTime,
                         Mapper.Map<List<TrackListItemDTO>>(_trackRepository.GetAll().OrderByDescending(m => m.Date)));
        }
    }
}