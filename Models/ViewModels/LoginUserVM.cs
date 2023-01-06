﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.ViewModels
{
    public class LoginUserVM
    {
        [Required]
        [Display(Name = "שם משתמש")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "סיסמא")]
        public string Password { get; set; }
        [Display(Name = "זכור אותי")]
        public bool RememberMe { get; set; }
    }
}