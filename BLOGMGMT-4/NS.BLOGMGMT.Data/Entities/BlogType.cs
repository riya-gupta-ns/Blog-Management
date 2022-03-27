using System;
using System.Collections.Generic;

namespace NS.BLOGMGMT.Data.Entities
{
    public partial class BlogType
    {
        public BlogType()
        {
            Blogs = new HashSet<Blog>();
        }

        public int TypeId { get; set; }
        public string? TypeName { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
