using AutoMapper;
using MeterReaderCMS.Infrastructure;
using MeterReaderCMS.Models.DTO.Notebook;
using MeterReaderCMS.Models.Entities;
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
    public class NotebooksController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private INotebookRepository _notebookRepository;

        public NotebooksController(INotebookRepository notebookRepository)
        {
            _notebookRepository = notebookRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddNotebook()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                logger.Error($"AddNotebook() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        [HttpPost]
        public ActionResult AddNotebook([Bind(Exclude = "Id")] NotebookDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //if (_notebookRepository.GetAll().Any(x => x.Number == model.Number))
                //{
                //    ModelState.AddModelError("CustomError", "מספר פנקס כבר קיים");
                //    return View(model);
                //}


                model.StreetName = !string.IsNullOrEmpty(model.StreetName) ? model.StreetName : "";
                Notebook dto = Mapper.Map<Notebook>(model);
                _notebookRepository.Add(dto);
                updateCache();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Error($"AddNotebook() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        [HttpGet]
        public ActionResult EditNotebook(int id)
        {
            try
            {            
                Notebook dto = _notebookRepository.Get(id);
               
                if (dto == null)
                {
                    return Content("הפנקס לא קיים");
                }

                ViewBag.Tracks = LoadTracks(id);

                EditNotebookDTO model = Mapper.Map<EditNotebookDTO>(dto);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.Error($"EditNotebook() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        [HttpPost]
        public ActionResult EditNotebook(EditNotebookDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Tracks = LoadTracks(model.Id);
                    return View(model);
                }

                if (_notebookRepository.NumberExists(model.Id,model.Number))
                {
                    ViewBag.Tracks = LoadTracks(model.Id);
                    ModelState.AddModelError("CustomError", "מספר פנקס כבר קיים.");
                    return View(model);
                }

                Notebook dto = _notebookRepository.Get(model.Id);
                if (dto == null)
                {
                    return Content("הפנקס לא קיים.");
                }
                else
                {
                    dto = Mapper.Map<Notebook>(model);
                    _notebookRepository.Update(dto);
                    updateCache();
                    ViewBag.Tracks = LoadTracks(dto.Id);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                logger.Error($"EditNotebook() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        [HttpGet]
        public ActionResult DeleteBoulder(int id)
        {
            try
            {
                Notebook notebookToDelete = _notebookRepository.Get(id);

                if (notebookToDelete.Tracks.Count() == 0)
                {
                    _notebookRepository.Delete(id);
                    updateCache();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Error($"DeleteBoulder() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        private void updateCache()
        {
            MemoryCacher.Delete(Constant.NotebookList);
            var notebooks = new List<NotebookDTO>();
            notebooks = Mapper.Map<List<NotebookDTO>>(_notebookRepository.GetAll().OrderByDescending(m => m.Number).ToList());
            MemoryCacher.Add(Constant.NotebookList, notebooks, DateTimeOffset.Now.AddMinutes(Constant.CacheTime));
        }

        private List<Track> LoadTracks(int id)
        {
            var tracks = _notebookRepository.GetNotebookTracks(id);
            return tracks;
        }
    }
}