using System.Collections;
using System.Collections.Generic;

namespace XtraSpurt.MultiDbSupport.Domains
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public ICollection Posts { get; } = new List<Post>();
    }
}