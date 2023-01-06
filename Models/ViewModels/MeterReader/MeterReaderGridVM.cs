using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.ViewModels.MeterReader
{
    public class MeterReaderGridVM
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int NoteBookNumber { get; set; }
        public int Called { get; set; }
        public int UnCalled { get; set; }
        public string StreetName { get; set; }
    }
}