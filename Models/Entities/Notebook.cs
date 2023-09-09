using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.Entities
{
    public class Notebook
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
    }
}