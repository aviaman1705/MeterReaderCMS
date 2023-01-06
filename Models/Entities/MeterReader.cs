using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.Entities
{
    public class MeterReader : BaseClass
    {
        private int _NotebookId;
        private int _UserId;

        public DateTime Date { get; set; }
        public int Called { get; set; }
        public int UnCalled { get; set; } = 0;
        public int Left { get; set; } = 0;
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