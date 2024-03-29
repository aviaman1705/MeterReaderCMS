﻿using MeterReaderCMS.Models.DTO.Street;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.DTO.Track
{
    public class EditTrackDTO
    {
        public int Id { get; set; }

        [Display(Name = "תאריך")]
        [Required(ErrorMessage = "חובה לבחור תאריך")]
        public string Date { get; set; }

        [Display(Name = "תיאור")]        
        public string Desc { get; set; }

        [Display(Name = "נקראו")]
        public int? Called { get; set; }

        [Display(Name = "נקראו לא")]
        public int? UnCalled { get; set; }

        [Display(Name = "מספר פנקס")]
        [Required(ErrorMessage = "חובה לבחור מספר פנקס")]
        public int NotebookId { get; set; }
        public int UserId { get; set; }
    }
}