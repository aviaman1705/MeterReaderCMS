using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Models.ViewModels;
using MeterReaderCMS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MeterReaderCMS.Controllers.api
{
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        private IUserRepository _userRepository;

        public TestController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("Registration")]
        public string Registration(UserVM model)
        {
            string msg = string.Empty;

            try
            {
                User userDTO = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.EmailAddress,
                    Username = model.Username,
                    Password = model.Password,
                    ActivationCode = Guid.NewGuid()
                };

                _userRepository.Add(userDTO);
                int id = userDTO.UserId;

                UserRole userRoleDTO = new UserRole()
                {
                    UserId = id,
                    RoleId = 2
                };

                _userRepository.AddUserRole(userRoleDTO);
                msg = "Data Inserted";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }

        [HttpPost]
        [Route("Login")]
        public string Login(LoginUserVM model)
        {
            string msg = string.Empty;
            try
            {
                var users = _userRepository.GetAll().ToList();
                if (users.Any(x => x.Username.Equals(model.Username) && x.Password.Equals(model.Password)))
                {
                    msg = "User is valid";
                }
                else
                {
                    msg = "User is Invalid";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }
    }
}
