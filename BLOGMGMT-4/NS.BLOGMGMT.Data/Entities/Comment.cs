using System;
using System.Collections.Generic;

namespace NS.BLOGMGMT.Data.Entities
{
    public partial class Comment
    {
        public long CommentId { get; set; }
        public long? BlogId { get; set; }
        public string? CommentContent { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Name { get; set; }

        public virtual Blog? Blog { get; set; }
    }
}
