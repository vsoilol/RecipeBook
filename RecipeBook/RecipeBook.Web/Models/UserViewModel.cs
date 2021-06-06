﻿using RecipeBook.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Web.Models
{
    public class UserViewModel
    {
        public string Password { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }
    }
}
