using MeterReaderCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReaderCMS.Repositories.Interfaces
{
    public interface ITrackRepository : IRepository<Track, int>
    {
         bool TrackExistsOnThisDate(DateTime time, int noteBookId);
    }
}
