using System;
using System.Collections.Generic;

namespace RequestsKeeper2.Models
{
    public partial class VisitorVisit
    {
        public int Id { get; set; }
        public int VisitorId { get; set; }
        public int VisitId { get; set; }

        public virtual Visit Visit { get; set; } = null!;
        public virtual Visitor Visitor { get; set; } = null!;
    }
}
