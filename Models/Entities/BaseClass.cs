using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.Entities
{
    public abstract class BaseClass
    {
        [Key]
        public virtual int Id { get; set; }
    }
}