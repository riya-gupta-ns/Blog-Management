using System;
using System.Collections.Generic;

namespace NS.BLOGMGMT.Data.Entities
{
    public partial class StaticContent
    {
        public int ContentId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? Category { get; set; }
    }
}
