using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Helper
{
    public class SysDataTablePager<T>
    {
        public SysDataTablePager(IEnumerable<T> items, int totalRecords, int totalDisplayRecords, string sEcho)
        {
            this.aaData = items;
            this.iTotalRecords = totalRecords;
            this.iTotalDisplayRecords = totalDisplayRecords;
            this.sEcho = sEcho;
        }

        public IEnumerable<T> aaData { get; set; }

        public int iTotalRecords { get; set; }

        public int iTotalDisplayRecords { get; set; }

        public string sEcho { get; set; }
    }
}