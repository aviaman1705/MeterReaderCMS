using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.Entities
{
    public class Street
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
    }
}