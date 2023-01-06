using MeterReaderCMS.Repositories.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MeterReaderCMS.Controllers.api
{
    public class HomeController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();        
        private IDashboardRepository _dashboardrRepository;

        public HomeController(IDashboardRepository dashboardRepository)
        {
            _dashboardrRepository = dashboardRepository;
        }

        public IHttpActionResult GetDashboardData()
        {
            var data = _dashboardrRepository.GetDashboardData(User.Identity.Name);
            return Ok(data);
        }
    }
}
