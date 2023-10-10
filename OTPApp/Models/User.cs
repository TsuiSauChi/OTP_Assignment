using System;
using System.Collections.Generic;

#nullable disable

namespace OTPApp.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Otp { get; set; }
        public int? Attempt { get; set; }
    }
}
