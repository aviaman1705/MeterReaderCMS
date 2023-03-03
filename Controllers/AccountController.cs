using AutoMapper;
using MeterReaderCMS.Infrastructure;
using MeterReaderCMS.Models.DTO.User;
using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Repositories.Interfaces;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Security;

namespace MeterReaderCMS.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class AccountController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                string username = User.Identity.Name;

                if (!string.IsNullOrEmpty(username))
                    return RedirectToAction("Index", "Home");

                return View();
            }
            catch (Exception ex)
            {
                logger.Error($"Login() {DateTime.Now}");
                logger.Error(ex);
                logger.Error("==============================");
                return null;
            }
        }

        [HttpPost]
        public ActionResult Login(LoginUserDTO model)
        {
            try
            {
                // Check model state
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // Check if the user is valid

                bool isvalid = false;
                var users = _userRepository.GetAll().ToList();
                if (users.Any(x => x.Username.Equals(model.Username) && x.Password.Equals(model.Password)))
                {
                    isvalid = true;
                }

                if (!isvalid)
                {
                    ModelState.AddModelError("CustomError", "שם משתמש וסיסמא אינם נכונים.");
                    return View(model);
                }
                else
                {
                    logger.Info("Login");
                    logger.Debug("Login Debug");
                    logger.Error($"Login() {DateTime.Now}");
                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Login() {DateTime.Now}");
                logger.Error(ex);
                logger.Error("==============================");
                return null;
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserDTO modelDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(modelDTO);
                }

                if (!modelDTO.Password.Equals(modelDTO.ConfirmPassword))
                {
                    ModelState.AddModelError("PasswordError", "הסיסמאות לא זהות");
                    return View(modelDTO);
                }

                if (_userRepository.GetAll().Any(x => x.Username.Equals(modelDTO.Username)))
                {
                    ModelState.AddModelError("UsernameError", $"שם המשתמש {modelDTO.Username} כבר קיים");
                    modelDTO.Username = "";
                    return View(modelDTO);
                }


                User user = Mapper.Map<User>(modelDTO);

                _userRepository.Add(user);
                int id = user.UserId;

                UserRole userRoleDTO = new UserRole()
                {
                    UserId = id,
                    RoleId = 1
                };

                _userRepository.AddUserRole(userRoleDTO);
                return View("Login");
            }
            catch (Exception ex)
            {
                logger.Error($"Register() {DateTime.Now}");
                logger.Error(ex);
                logger.Error("==============================");
                return null;
            }
        }
        
        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //MemoryCacher.Delete(Constant.BoulderList);
            return Redirect("Login");
        }

        [Authorize]
        public ActionResult UserNavPartial()
        {
            try
            {
                // Get username
                string username = User.Identity.Name;

                // Declare model
                UserNavPartialDTO model;

                // Get the user
                User dto = _userRepository.Get(username);

                // Build the model
                model = new UserNavPartialDTO()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                };

                // Return partial view with model
                return PartialView(model);
            }
            catch (Exception ex)
            {
                logger.Error($"UserNavPartial() {DateTime.Now}");
                logger.Error(ex);
                logger.Error("==============================");
                return null;
            }
        }

        [HttpGet]
        [ActionName("user-profile")]
        [Authorize]
        public ActionResult UserProfile()
        {
            try
            {
                // Get username
                string username = User.Identity.Name;

                // declare model
                UserProfileDTO model;

                // get user
                User dto = _userRepository.Get(username);

                // build model
                model = Mapper.Map<UserProfileDTO>(dto);

                // Return view with model
                return View("UserProfile", model);
            }
            catch (Exception ex)
            {
                logger.Error($"UserProfile() {DateTime.Now}");
                logger.Error(ex);
                logger.Error("==============================");
                return null;
            }
        }

        [HttpPost]
        [ActionName("user-profile")]
        [Authorize]
        public ActionResult UserProfile(UserProfileDTO model)
        {
            try
            {
                //1.Check if the model is valid
                if (!ModelState.IsValid)
                {
                    return View("UserProfile", model);
                }

                string username = User.Identity.Name;
   
                User dto = Mapper.Map<User>(model);
                
                if (dto != null)
                {
                    _userRepository.Update(dto);
                    FormsAuthentication.SetAuthCookie(dto.Username, true);
                }
                
                return Redirect("~/account/User-profile");
            }
            catch (Exception ex)
            {
                logger.Error($"UserProfile() {DateTime.Now}");
                logger.Error(ex);
                logger.Error("==============================");
                return null;
            }
        }
    }
}