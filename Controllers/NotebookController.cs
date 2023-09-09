using AutoMapper;
using MeterReaderCMS.Helper;
using MeterReaderCMS.Infrastructure;
using MeterReaderCMS.Models.DTO.Notebook;
using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Repositories.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace MeterReaderCMS.Controllers
{
    public class NotebookController : Controller
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private INotebookRepository _notebookRepository;

        public NotebookController(Logger logger, INotebookRepository notebookRepository)
        {
            _logger = logger;
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
                _logger.Error($"AddNotebook() {DateTime.Now}");
                _logger.Error(ex.Message);
                _logger.Error("==============================");
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

                Notebook entity = Mapper.Map<Notebook>(model);
                if (!_notebookRepository.NotebookExsist(entity.Number))
                {

                    _notebookRepository.Add(entity);
                    updateCache();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("CustomError", "לא ניתן להוסיף מספר פנקס שכבר קיים.");
                    return View();
                }

            }
            catch (Exception ex)
            {
                _logger.Error($"AddNotebook() {DateTime.Now}");
                _logger.Error(ex.Message);
                _logger.Error("==============================");
                return null;
            }
        }

        [HttpGet]
        public ActionResult EditNotebook(int id)
        {
            try
            {
                NotebookDTO model;
                Notebook dto = _notebookRepository.Get(id);

                if (dto == null)
                {
                    return Content("מסלול לא קיים");
                }

                model = Mapper.Map<NotebookDTO>(dto);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.Error($"EditNotebook() {DateTime.Now}");
                _logger.Error(ex.Message);
                _logger.Error("==============================");
                return null;
            }
        }

        [HttpPost]
        public ActionResult EditNotebook(NotebookDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (_notebookRepository.GetAll().Any(x => x.Number == model.Number && x.Id != model.Id))
                {
                    ModelState.AddModelError("CustomError", "לא ניתן לשנות למספר פנקס שכבר קיים.");
                    return View(model);
                }
                else
                {
                    Notebook dto = Mapper.Map<Notebook>(model);
                    _notebookRepository.Update(dto);
                    updateCache();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"EditNotebook() {DateTime.Now}");
                _logger.Error(ex.Message);
                _logger.Error("==============================");
                return null;
            }
        }

        [HttpGet]
        public ActionResult DeleteNotebook(int id)
        {
            try
            {
                _notebookRepository.Delete(id);
                updateCache();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.Error($"DeleteNotebook() {DateTime.Now}");
                _logger.Error(ex.Message);
                _logger.Error("==============================");
                return null;
            }
        }

        private void updateCache()
        {
            Utilities.updateCache<NotebookDTO>(
                         Constant.NotebookList,
                         Constant.CacheTime,
                         Mapper.Map<List<NotebookDTO>>(_notebookRepository.GetAll().OrderBy(m => m.Number).ToList()));
        }
    }
}
