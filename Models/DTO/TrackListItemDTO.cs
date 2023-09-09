using MeterReaderCMS.Models.DTO.Street;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.DTO
{
    public class TrackListItemDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int NotebookNumber { get; set; }

        public string Desc { get; set; }

        public int Called { get; set; }

        public int UnCalled { get; set; }
    }
}