using AutoMapper;
using MeterReaderCMS.Models.DTO.Home;
using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Repositories.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        public async Task<HttpResponseMessage> GetDashboardData()
        {
            try
            {
                var dashboardData =  Mapper.Map<List<CallSummaryDTO>>(await _dashboardrRepository.GetDashboardData("admin"));

                if (dashboardData == null)
                {
                    logger.Info($"GetDashboardData: Dashboard data return null");
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                logger.Info($"GetDashboardData: Retrive dashboard data");
                return Request.CreateResponse(HttpStatusCode.OK, dashboardData);
            }
            catch (Exception ex)
            {
                logger.Fatal($"GetDashboardData: {ex.Message}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }
    }
}
