using System;
using Microsoft.AspNetCore.Identity;

namespace XtraSpurt.MultiDbSupport.Domains
{
    public class XtraSpurtRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}