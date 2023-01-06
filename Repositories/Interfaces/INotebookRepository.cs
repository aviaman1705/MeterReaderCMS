using MeterReaderCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MeterReaderCMS.Repositories.Interfaces
{
    public interface INotebookRepository : IRepository<Notebook, int>
    {
        List<Track> GetNotebookTracks(int id);
        bool NumberExists(int id, int number);
        List<SelectListItem> LoadNoteBooks();
    }
}
