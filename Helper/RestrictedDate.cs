using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Helper
{
    public class RestrictedDate : ValidationAttribute
    {
        public override bool IsValid(object currentDate)
        {
            bool isValid = false;
            DateTime date;

            if (currentDate != null)
            {
                date = (DateTime)currentDate;
                isValid = date < DateTime.Now;
            }
            return isValid;
        }
    }
}