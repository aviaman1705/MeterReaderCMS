using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.DTO.Track
{
	public class CreateTrackDTO
	{
        [Display(Name = "תאריך")]
        [Required(ErrorMessage = "חובה לבחור תאריך")]
        public string Date { get; set; }

        [Display(Name = "תיאור")]
        [Required(ErrorMessage = "חובה להזין תיאור")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Desc { get; set; }

        [Display(Name = "נקראו")]
        public int? Called { get; set; }

        [Display(Name = "נקראו לא")]
        public int? UnCalled { get; set; }
    }
}