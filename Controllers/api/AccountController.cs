using MeterReaderCMS.Repositories.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using MeterReaderCMS.Helper;
using System.Web;
using System.Web.Http.Cors;
using MeterReaderCMS.Models.DTO.User;

namespace MeterReaderCMS.Controllers.api
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [ValidateAntiForgeryToken]
        public IHttpActionResult Login(LoginUserDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(model);

                string encryptedPwd = model.Password;
                var userPassword = Convert.ToString(ConfigurationManager.AppSettings["config:Password"]);
                var userName = Convert.ToString(ConfigurationManager.AppSettings["config:Username"]);

                var currentUser = _userRepository.GetAll().FirstOrDefault(x => x.Username.Equals(model.Username) && x.Password.Equals(model.Password));

                if (currentUser != null)
                {
                    //var roles = new string[] { "SuperAdmin", "Admin" };
                    var jwtSecurityToken = Authentication.GenerateJwtToken(model.Username, currentUser.Roles.Select(x => x.RoleName).ToList());
                    HttpContext.Current.Session["LoginedIn"] = userName;
                    return Ok(new { token = jwtSecurityToken });
                }

                return NotFound();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
