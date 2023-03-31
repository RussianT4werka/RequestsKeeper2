using System;
using System.Collections.Generic;

namespace RequestsKeeper2.Models
{
    public partial class Visitor
    {
        public Visitor()
        {
            VisitorRequests = new HashSet<VisitorRequest>();
            VisitorVisits = new HashSet<VisitorVisit>();
        }

        public int Id { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = null!;
        public DateTime? DoB { get; set; }
        public string? PassportSeries { get; set; }
        public string? PassportNumber { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public byte[]? Photo { get; set; }
        public byte[]? ScanPassport { get; set; }
        public string? Organisation { get; set; }
        public string? Note { get; set; }

        public virtual ICollection<VisitorRequest> VisitorRequests { get; set; }
        public virtual ICollection<VisitorVisit> VisitorVisits { get; set; }
    }
}
