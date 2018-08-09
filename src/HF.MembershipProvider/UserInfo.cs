using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HF.MembershipProvider
{
    public class UserInfo
    {
        [DBColumn]
        public int UserId { get; set; }
        [DBColumn]
        public string Name { get; set; }
        [DBColumn]
        public string Password { get; set; }
        [DBColumn]
        public string Email { get; set; }
        [DBColumn]
        public string PasswordQuestion { get; set; }

    }
}