using MeterReaderCMS.Models.DTO;
using MeterReaderCMS.Models.DTO.Track;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.DTO.Notebook
{
    public class NotebookDTO
    {
        public int Id { get; set; }

        [Display(Name = "מספר פנקס")]
        public int? Number { get; set; }

        [Display(Name = "רחוב")]
        [Required(ErrorMessage = "חובה להזין רחוב")]
        public string StreetName { get; set; }
        
        public virtual List<TrackDTO> Tracks { get; set; }
    }
}