using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Repositories.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeterReaderCMS.Repositories.Implementation
{
    public class DashboardRepository : IDashboardRepository
    {
        private MeterReaderDB context = new MeterReaderDB();

        public async Task<List<CallSummary>> GetDashboardData(string userName)
        {
            var dashboardData = await context.Database.SqlQuery<CallSummary>("SP_GetDashboardData @UserName = {0}", userName).ToListAsync();
            return dashboardData;
        }
    }
}
