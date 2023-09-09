using MeterReaderCMS.Models.DTO.Notebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.DTO.Track
{
    public class TrackNotebookDTO
    {
        public int Id { get; set; }
        public int TrackID { get; set; }
        public int NotebbookID { get; set; }
        public TrackDTO Track { get; set; }
        public NotebookDTO Notebook { get; set; }
        public string Desc { get; set; }
    }
}