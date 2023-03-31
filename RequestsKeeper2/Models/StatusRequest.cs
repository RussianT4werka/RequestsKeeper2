using System;
using System.Collections.Generic;

namespace RequestsKeeper2.Models
{
    public partial class StatusRequest
    {
        public StatusRequest()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }
    }
}
