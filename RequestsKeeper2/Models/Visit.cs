using System;
using System.Collections.Generic;

namespace RequestsKeeper2.Models
{
    public partial class Visit
    {
        public Visit()
        {
            VisitorVisits = new HashSet<VisitorVisit>();
        }

        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int WorkerId { get; set; }
        public string? GroupNumber { get; set; }

        public virtual Worker Worker { get; set; } = null!;
        public virtual ICollection<VisitorVisit> VisitorVisits { get; set; }
    }
}
