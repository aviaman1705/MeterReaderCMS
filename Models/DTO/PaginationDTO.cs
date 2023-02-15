using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.DTO
{
    public class PaginationDTO
    {
        public string SortColumn{ get; set; }

        public string SortType { get; set; }

        //public int Page { get; set; } = 1;

        //private int recordsPerPage = 10;
        //private readonly int maxRecordsPerPage = 50;

        //public int RecrdsPerPage
        //{
        //    get
        //    {
        //        return recordsPerPage;
        //    }
        //    set
        //    {
        //        recordsPerPage = (value > maxRecordsPerPage) ? maxRecordsPerPage : value;
        //    }
        //}
    }
}