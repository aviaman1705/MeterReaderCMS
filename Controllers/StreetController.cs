using AutoMapper;
using MeterReaderCMS.Helper;
using MeterReaderCMS.Infrastructure;
using MeterReaderCMS.Models.DTO.Street;
using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Repositories.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MeterReaderCMS.Controllers
{
    public class StreetController : Controller
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private IStreetRepository _streetRepository;

        public StreetController(Logger logger, IStreetRepository streetRepository)
        {
            _logger = logger;
            _streetRepository = streetRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddStreet()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.Error($"AddStreet() {DateTime.Now}");
                _logger.Error(ex.Message);
                _logger.Error("==============================");
                return null;
            }
        }

        [HttpPost]
        public ActionResult AddStreet([Bind(Exclude = "Id")] StreetDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Street entity = Mapper.Map<Street>(model);
                _streetRepository.Add(entity);
                updateCache();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.Error($"AddStreet() {DateTime.Now}");
                _logger.Error(ex.Message);
                _logger.Error("==============================");
                return null;
            }
        }

        [HttpGet]
        public ActionResult EditStreet(int id)
        {
            try
            {
                StreetDTO model;
                Street dto = _streetRepository.Get(id);

                if (dto == null)
                {
                    return Content("מסלול לא קיים");
                }

                model = Mapper.Map<StreetDTO>(dto);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.Error($"EditStreet() {DateTime.Now}");
                _logger.Error(ex.Message);
                _logger.Error("==============================");
                return null;
            }
        }

        [HttpPost]
        public ActionResult EditStreet(StreetDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Street dto = _streetRepository.Get(model.Id);
                if (dto == null)
                {
                    return Content("המסלול לא קיים");
                }
                else
                {
                    dto = Mapper.Map<Street>(model);

                    _streetRepository.Update(dto);
                    updateCache();

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"EditStreet() {DateTime.Now}");
                _logger.Error(ex.Message);
                _logger.Error("==============================");
                return null;
            }
        }

        [HttpGet]
        public ActionResult DeleteStreet(int id)
        {
            try
            {
                _streetRepository.Delete(id);
                updateCache();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.Error($"DeleteStreet() {DateTime.Now}");
                _logger.Error(ex.Message);
                _logger.Error("==============================");
                return null;
            }
        }

        private void updateCache()
        {
            Utilities.updateCache<StreetDTO>(
                         Constant.StreetList,
                         Constant.CacheTime,
                         Mapper.Map<List<StreetDTO>>(_streetRepository.GetAll().OrderBy(m => m.Title).ToList()));
        }
    }
}