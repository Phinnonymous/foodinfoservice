﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.DTOs
{
    public class UpdatePasswordDTO
    {
        public string Username { get; set; }
        public string NewPassword { get; set; }
    }
}
