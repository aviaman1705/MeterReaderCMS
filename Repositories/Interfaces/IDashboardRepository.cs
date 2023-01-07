using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Models.ViewModels.MeterReader;
using System.Collections.Generic;

namespace MeterReaderCMS.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        List<CallSummary> GetDashboardData(string userName);
    }
}
