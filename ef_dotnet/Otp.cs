using System;
using System.Collections.Generic;

#nullable disable

namespace ef_dotnet
{
    public partial class Otp
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Otp1 { get; set; }
        public int? Attempt { get; set; }

        public virtual User User { get; set; }
    }
}
