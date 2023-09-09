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

        [Display(Name ="פנקס")]
        [Required(ErrorMessage ="חובה להזין מספר פנקס")]
        [Range(1, 9999,ErrorMessage ="נא הזן מספר בטווח הספרות בין 1 ל-9999")]
        public int Number { get; set; }

        public virtual ICollection<TrackDTO> Tracks { get; set; }
    }
}