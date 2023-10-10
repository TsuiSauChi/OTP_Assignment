using System;
using System.Collections.Generic;

#nullable disable

namespace ef_dotnet
{
    public partial class UserStatus
    {
        public UserStatus()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
