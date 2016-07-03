﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace KindergartentManagerment.Models
{
    public class AspNetUserLogin
    {
        [Key]
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserId { get; set; }

        public AspNetUser AspNetUser { get; set; }
    }
}