using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MeterReaderCMS.Repositories.Implementation
{
    public class NotebookRepository : INotebookRepository
    {
        private MeterReaderDB context = new MeterReaderDB();

        public void Add(Notebook item)
        {
            context.Notesbooks.Add(item);
            context.SaveChanges();
        }

        public IQueryable<Notebook> GetAll()
        {
            return context.Notesbooks;
        }

        public Notebook Get(int id)
        {
            return context.Notesbooks.Include(x => x.Tracks).FirstOrDefault(x => x.Id == id);
        }

        public bool Update(Notebook item)
        {
            bool isUpdated = false;
            Notebook entity = context.Notesbooks.FirstOrDefault(p => p.Id == item.Id);
            if (entity != null)
            {
                entity.Number = item.Number;
                entity.StreetName = item.StreetName;
                context.SaveChanges();
                isUpdated = true;
            }
            return isUpdated;
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;
            Notebook entity = context.Notesbooks.FirstOrDefault(p => p.Id == id);
            if (entity != null)
            {
                context.Notesbooks.Remove(entity);
                context.SaveChanges();
                isDeleted = true;
            }
            return isDeleted;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public List<SelectListItem> LoadNoteBooks()
        {
            var noteBooks = context.Notesbooks
               .OrderBy(x => x.Number)
               .Select(x => new SelectListItem()
               {
                   Text = x.Number.ToString(),
                   Value = x.Id.ToString()
               }).ToList();

            return noteBooks;
        }

        public bool NumberExists(int id, int number)
        {
            return context.Notesbooks.Any(x => x.Id != id && x.Number == number);
        }    
    }
}