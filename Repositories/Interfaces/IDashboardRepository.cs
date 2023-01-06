using MeterReaderCMS.Models.ViewModels.MeterReader;
using System.Collections.Generic;

namespace MeterReaderCMS.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        List<MeterReaderDashboardVM> GetDashboardData(string userName);
    }
}
