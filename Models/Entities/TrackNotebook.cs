using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.Entities
{
    public class TrackNotebook
    {
        public int Id { get; set; }
        public int TrackID { get; set; }

        public int NotebbookID { get; set; }

        [ForeignKey("TrackID")]
        public Track Track { get; set; }

        [ForeignKey("NotebbookID")]
        public Notebook Notebook { get; set; }

        public string Desc { get; set; }
    }
}