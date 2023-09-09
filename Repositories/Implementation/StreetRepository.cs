using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Repositories.Implementation
{
    public class StreetRepository : IStreetRepository
    {
        private MeterReaderDB context = new MeterReaderDB();
        public void Add(Street item)
        {
            context.Streets.Add(item);
            context.SaveChanges();
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;
            Street entity = context.Streets.FirstOrDefault(p => p.Id == id);
            context.Streets.Remove(entity);
            context.SaveChanges();
            isDeleted = true;

            return isDeleted;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public Street Get(int id)
        {
            return context.Streets.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Street> GetAll()
        {
            return context.Streets;
        }

        public bool Update(Street item)
        {
            bool isUpdated = false;
            Street entity = context.Streets.FirstOrDefault(p => p.Id == item.Id);
            if (entity != null)
            {
                entity.Number = item.Number;
                entity.Title = item.Title;
                context.SaveChanges();
                isUpdated = true;
            }
            return isUpdated;
        }
    }
}