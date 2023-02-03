using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.Entities
{
    public class CallSummary
    {
        public int Called { get; set; }

        public int UnCalled { get; set; }

        public string Month { get; set; }
    }
}