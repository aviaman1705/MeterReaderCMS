using AutoMapper;
using MeterReaderCMS.Infrastructure;
using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Models.ViewModels.MeterReader;
using MeterReaderCMS.Repositories.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

namespace MeterReaderCMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BouldersController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IMeterReaderRepository _meterReaderRepository;
        private INotebookRepository _notebookRepository;
        private IUserRepository _userRepository;

        public BouldersController(IMeterReaderRepository meterReaderRepository, INotebookRepository notebookRepository, IUserRepository userRepository)
        {
            _meterReaderRepository = meterReaderRepository;
            _notebookRepository = notebookRepository;
            _userRepository = userRepository;
        }

        public ActionResult Index()
        {
            SendEmail("aviaman1705@gmail.com", "aviaman1705@gmail.com", "test");
            return View();
        }

        [HttpGet]
        public ActionResult AddBoulder()
        {
            try
            {
                ViewBag.NoteBooks = LoadNoteBooks();
                return View();
            }
            catch (Exception ex)
            {
                logger.Error($"AddBoulder() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        [HttpPost]
        public ActionResult AddBoulder([Bind(Exclude = "Id")] AddBoulderVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.NoteBooks = LoadNoteBooks();
                    return View(model);
                }

                DateTime boulderDate = DateTime.ParseExact(model.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (_meterReaderRepository.MeterReaderExists(boulderDate, model.NoteBookID))
                {
                    ModelState.AddModelError("CustomError", "תאריך כבר קיים.");
                    ViewBag.NoteBooks = LoadNoteBooks();
                    return View(model);
                }

                MeterReader dto = Mapper.Map<MeterReader>(model);
                User currentUser = _userRepository.GetAll().FirstOrDefault(x => x.Username == User.Identity.Name);

                if (currentUser != null)
                {
                    dto.UserId = currentUser.UserId;
                    _meterReaderRepository.Add(dto);
                    updateCache();
                    ViewBag.NoteBooks = LoadNoteBooks();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Error($"AddBoulder() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        [HttpGet]
        public ActionResult EditBoulder(int id)
        {
            try
            {
                EditBoulderVM model;
                MeterReader dto = _meterReaderRepository.Get(id);

                if (dto == null)
                {
                    return Content("הסקלה לא קיימת");
                }

                ViewBag.NoteBooks = LoadNoteBooks();
                model = Mapper.Map<EditBoulderVM>(dto);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.Error($"EditBoulder() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        [HttpPost]
        public ActionResult EditBoulder(EditBoulderVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.NoteBooks = LoadNoteBooks();
                    return View(model);
                }

                DateTime boulderDate = DateTime.ParseExact(model.Date, "dd/MM/yyyy", null);
                MeterReader dto = _meterReaderRepository.Get(model.Id);

                if (dto == null)
                {
                    return Content("הסקלה לא קיימת");
                }
                else
                {
                    dto = Mapper.Map<MeterReader>(model);
                    dto.Date = boulderDate;
                    _meterReaderRepository.Update(dto);
                    updateCache();
                    ViewBag.NoteBooks = LoadNoteBooks();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                logger.Error($"EditBoulder() {DateTime.Now}");
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
                _meterReaderRepository.Delete(id);
                updateCache();
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

        private List<SelectListItem> LoadNoteBooks()
        {
            var noteBooks = _notebookRepository.GetAll()
                .OrderBy(x=>x.Number)
                .Select(x => new SelectListItem()
                {
                    Text = x.Number.ToString(),
                    Value = x.Id.ToString()
                }).ToList();

            return noteBooks;
        }

        private void updateCache()
        {
            MemoryCacher.Delete(Constant.BoulderList);
            var metersReaders = new List<MeterReaderGridVM>();
            metersReaders = Mapper.Map<List<MeterReaderGridVM>>(_meterReaderRepository.GetAll().OrderByDescending(m => m.Date).ToList());
            MemoryCacher.Add(Constant.BoulderList, metersReaders, DateTimeOffset.Now.AddMinutes(Constant.CacheTime));
        }

        public bool SendEmail(string toEmail, string subject, string emailBody)
        {
            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString(); ;
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);


                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}