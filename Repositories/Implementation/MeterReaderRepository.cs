using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace MeterReaderCMS.Repositories.Implementation
{
    public class MeterReaderRepository : IMeterReaderRepository
    {
        private MeterReaderDB context = new MeterReaderDB();

        public void Add(MeterReader item)
        {
            context.MetersReaders.Add(item);
            context.SaveChanges();
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;
            MeterReader entity = context.MetersReaders.FirstOrDefault(p => p.Id == id);
            if (entity != null)
            {
                context.MetersReaders.Remove(entity);
                context.SaveChanges();
                isDeleted = true;
            }
            return isDeleted;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public MeterReader Get(int id)
        {
            return context.MetersReaders.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<MeterReader> GetAll()
        {
            return context.MetersReaders.Include(x => x.NoteBook);
        }

        public bool MeterReaderExists(DateTime time, int noteBookId)
        {
            return GetAll().Any(x => x.Date.Year == time.Year && x.Date.Month == time.Month && x.Date.Day == time.Day && x.NotebookId == noteBookId);
        }

        public bool Update(MeterReader item)
        {
            bool isUpdated = false;
            MeterReader entity = context.MetersReaders.FirstOrDefault(p => p.Id == item.Id);
            if (entity != null)
            {
                entity.NotebookId = item.NotebookId;
                entity.Called = item.Called;
                entity.Date = item.Date;
                entity.Left = item.Left;
                entity.UnCalled = item.UnCalled;
                context.SaveChanges();
                isUpdated = true;
            }
            return isUpdated;
        }
    }
}