﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.ViewModels
{
    public class PageClientVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }        

        public int Sorting { get; set; }
    }
}