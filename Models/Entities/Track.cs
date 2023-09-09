using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.Entities
{
    public class Track
    {
        public int Id { get; set; }

        public int ElectricityMeterCalled { get; set; }

        public int ElectricityMeterUnCalled { get; set; }

        public string Desc { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int NotebookId { get; set; }

        [ForeignKey("NotebookId")]
        public virtual Notebook Notebook { get; set; }
    }
}