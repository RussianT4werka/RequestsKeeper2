using System;
using System.Collections.Generic;

namespace RequestsKeeper2.Models
{
    public partial class VisitorRequest
    {
        public int Id { get; set; }
        public int VisitorId { get; set; }
        public int RequestId { get; set; }

        public virtual Request Request { get; set; } = null!;
        public virtual Visitor Visitor { get; set; } = null!;
    }
}
