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
        [Required(ErrorMessage = "חובה להזין מספר פנקס")]
        [Range(1, int.MaxValue, ErrorMessage = "הזן ערך גדול מ- 1")]
        public int Number { get; set; }

        [Display(Name = "רחוב")]       
        public string StreetName { get; set; }
        
        public virtual List<TrackDTO> Tracks { get; set; }
    }
}