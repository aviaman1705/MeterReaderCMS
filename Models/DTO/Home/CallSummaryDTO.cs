using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.DTO.Home
{
    public class CallSummaryDTO
    {
        public int called { get; set; }
        public int uncalled { get; set; }
        public string month { get; set; }
    }
}