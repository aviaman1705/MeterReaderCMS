using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.ViewModels
{
    public class UserProfileDTO
    {
        public int Id { get; set; }

        [Display(Name = "שם פרטי")]
        [Required(ErrorMessage = "חובה להזין שם פרטי")]
        public string FirstName { get; set; }
       
        [Display(Name = "שם משפחה")]
        [Required(ErrorMessage = "חובה להזין שם משפחה")]
        public string LastName { get; set; }
        
        [Display(Name = "מייל")]
        [Required(ErrorMessage = "חובה להזין מייל")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        
        [Display(Name = "שם משתמש")]
        [Required(ErrorMessage = "חובה להזין שם משתמש")]
        public string Username { get; set; }

        [Display(Name = "סיסמא")]
        [Required(ErrorMessage = "חובה להזין סיסמא")]
        public string Password { get; set; }

        [Display(Name = "אימות סיסמא")]
        [Required(ErrorMessage = "חובה להזין אימות סיסמא")]
        public string ConfirmPassword { get; set; }
    }
}