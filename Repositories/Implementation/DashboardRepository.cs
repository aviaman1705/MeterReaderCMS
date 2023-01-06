using MeterReaderCMS.Models.ViewModels.MeterReader;
using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Repositories.Interfaces;
using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace MeterReaderCMS.Repositories.Implementation
{
    public class DashboardRepository : IDashboardRepository
    {
        private MeterReaderDB context = new MeterReaderDB();

        public List<MeterReaderDashboardVM> GetDashboardData(string userName)
        {
            var dashboardData = context.Database.SqlQuery<MeterReaderDashboardVM>("SP_GetDashboardData @UserName = {0}", userName).ToList();
            return dashboardData;
        }
    }
}
