using System;
using System.Collections.Generic;

#nullable disable

namespace OTPApp.Models
{
    public partial class User
    {
        public User()
        {
            Otps = new HashSet<Otp>();
        }

        public int Id { get; set; }
        public int? StatusId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public virtual UserStatus Status { get; set; }
        public virtual ICollection<Otp> Otps { get; set; }
    }
}
