using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.Entities
{
    public class Track
    {
        private int _NotebookId;
        private int _UserId;

        public int Id { get; set; }

        public int ElectricityMeterCalled { get; set; }

        public int ElectricityMeterUnCalled { get; set; }

        public string Desc { get; set; }

        public DateTime Date { get; set; }

        public int NotebookId
        {
            get { return _NotebookId; }
            set { _NotebookId = value; }
        }

        [ForeignKey("NotebookId")]
        public virtual Notebook NoteBook { get; set; }

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}