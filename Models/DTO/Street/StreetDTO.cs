using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.DTO.Street
{
    public class StreetDTO
    {
        public int Id { get; set; }

        [Display(Name = "שם")]
        [Required(ErrorMessage = "חובה להזין שם רחוב")]
        public string Title { get; set; }

        [Display(Name = "מספר")]
        public int Number { get; set; }

        public int TracksCount { get; set; }
    }
}