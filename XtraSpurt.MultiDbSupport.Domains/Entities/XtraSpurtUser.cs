using System;
using Microsoft.AspNetCore.Identity;

namespace XtraSpurt.MultiDbSupport.Domains
{
    public class XtraSpurtUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}