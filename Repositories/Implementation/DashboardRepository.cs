using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Repositories.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace MeterReaderCMS.Repositories.Implementation
{
    public class DashboardRepository : IDashboardRepository
    {
        private MeterReaderDB context = new MeterReaderDB();

        public List<CallSummary> GetDashboardData(string userName)
        {
            var dashboardData = context.Database.SqlQuery<CallSummary>("SP_GetDashboardData @UserName = {0}", userName).ToList();
            return dashboardData;
        }
    }
}
