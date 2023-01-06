using MeterReaderCMS.Models.Entities;
using System;

namespace MeterReaderCMS.Repositories.Interfaces
{
   public interface IMeterReaderRepository:IRepository<MeterReader,int>
    {
        bool MeterReaderExists(DateTime time,int noteBookId);
    }
}
