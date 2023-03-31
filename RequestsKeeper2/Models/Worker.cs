using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestsKeeper2.Models
{
    public partial class Worker
    {
        public Worker()
        {
            Requests = new HashSet<Request>();
            Visits = new HashSet<Visit>();
        }

        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public int? SubDivisionId { get; set; }
        public int? DepartmentId { get; set; }
        public int? Code { get; set; }

        public virtual Department? Department { get; set; }
        public virtual SubDivision? SubDivision { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }

        [NotMapped]
        public string FIO => string.Join(' ', Surname, Name, Patronymic); 
    }
}
