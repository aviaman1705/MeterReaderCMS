using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace MeterReaderCMS.Repositories.Implementation
{
    public class TrackRepository : ITrackRepository
    {
        private MeterReaderDB context = new MeterReaderDB();
        public void Add(Track item)
        {
            context.Tracks.Add(item);
            context.SaveChanges();
        }
        public bool Delete(int id)
        {
            bool isDeleted = false;
            Track entity = context.Tracks.FirstOrDefault(p => p.Id == id);
            if (entity != null)
            {
                context.Tracks.Remove(entity);
                context.SaveChanges();
                isDeleted = true;
            }
            return isDeleted;
        }
        public void Dispose()
        {
            context.Dispose();
        }
        public Track Get(int id)
        {
            return context.Tracks.FirstOrDefault(x => x.Id == id);
        }
        public IQueryable<Track> GetAll()
        {
            return context.Tracks.Include(x => x.User).Include(x => x.Notebook);
        }
        public bool Update(Track item)
        {
            bool isUpdated = false;
            Track entity = context.Tracks.FirstOrDefault(p => p.Id == item.Id);
            if (entity != null)
            {
                entity.Date = item.Date;
                entity.Desc = !string.IsNullOrEmpty(item.Desc) ? item.Desc : entity.Desc;
                entity.ElectricityMeterCalled = item.ElectricityMeterCalled;
                entity.ElectricityMeterUnCalled = item.ElectricityMeterUnCalled;
                context.SaveChanges();
                isUpdated = true;
            }
            return isUpdated;
        }
    }
}