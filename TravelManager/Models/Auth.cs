﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelManager.Models
{
    public class AuthData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public long CurrencyId { get; set; }
    }
}
