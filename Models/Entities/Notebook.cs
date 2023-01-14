using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.Entities

{
    public class Notebook : BaseClass
    {
        public int Number { get; set; }
        public string StreetName { get; set; }
        public virtual List<Track> Tracks { get; set; }
    }
}