using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.Entities
{
    public class SearchItem
    {
        public int Item { get; set; }
        public string Link { get; set; }
        public int TrackDone { get; set; }
    }
}