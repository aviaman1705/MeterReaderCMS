using MeterReaderCMS.Models.DTO.Notebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.DTO.Track
{
    public class TrackDTO
    {
        public int Id { get; set; }

        public int ElectricityMeterCalled { get; set; }

        public int ElectricityMeterUnCalled { get; set; }

        public DateTime Date { get; set; }

        public int NotebookId{ get; set; }
        
        public virtual NotebookDTO NoteBook { get; set; }
    }
}