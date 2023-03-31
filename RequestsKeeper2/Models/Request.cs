using System;
using System.Collections.Generic;

namespace RequestsKeeper2.Models
{
    public partial class Request
    {
        public Request()
        {
            VisitorRequests = new HashSet<VisitorRequest>();
        }

        public int Id { get; set; }
        public int TypeRequestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string TargetVisit { get; set; } = null!;
        public int WorkerId { get; set; }
        public int StatusRequestId { get; set; }
        public string? Cause { get; set; }

        public virtual StatusRequest StatusRequest { get; set; } = null!;
        public virtual TypeRequest TypeRequest { get; set; } = null!;
        public virtual Worker Worker { get; set; } = null!;
        public virtual ICollection<VisitorRequest> VisitorRequests { get; set; }
    }
}
