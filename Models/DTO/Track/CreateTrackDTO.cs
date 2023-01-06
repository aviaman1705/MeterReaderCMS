﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.DTO.Track
{
	public class CreateTrackDTO
	{
        [Display(Name = "פנקס")]
        [Required(ErrorMessage = "חובה לבחור פנקס")]
        public int NoteBookID { get; set; }

        [Display(Name = "תאריך")]
        [Required(ErrorMessage = "חובה לבחור תאריך")]
        public string Date { get; set; }

        [Display(Name = "נקראו")]
        public int? Called { get; set; }

        [Display(Name = "נקראו לא")]
        public int? UnCalled { get; set; }
    }
}