using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Repositories.Implementation
{
    public class NotebookRepository : INotebookRepository
    {
        private MeterReaderDB context = new MeterReaderDB();
        public void Add(Notebook item)
        {
            context.Notebooks.Add(item);
            context.SaveChanges();
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;
            Notebook entity = context.Notebooks.FirstOrDefault(p => p.Id == id);

            context.Notebooks.Remove(entity);
            context.SaveChanges();
            isDeleted = true;

            return isDeleted;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public Notebook Get(int id)
        {
            return context.Notebooks.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Notebook> GetAll()
        {
            return context.Notebooks;
        }
        public bool Update(Notebook item)
        {
            bool isUpdated = false;
            Notebook entity = context.Notebooks.FirstOrDefault(p => p.Id == item.Id);
            if (entity != null)
            {
                entity.Number = item.Number;
                context.SaveChanges();
                isUpdated = true;
            }
            return isUpdated;
        }

        public bool NotebookExsist(int number)
        {
            return context.Notebooks.Any(x => x.Number == number);
        }
    }
}