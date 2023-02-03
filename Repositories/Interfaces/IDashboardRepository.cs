using MeterReaderCMS.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeterReaderCMS.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<CallSummary>> GetDashboardData(string userName);
    }
}
