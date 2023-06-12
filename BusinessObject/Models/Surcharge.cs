using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Surcharge
    {
        public Surcharge()
        {
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public double? Fee { get; set; }

        public virtual ICollection<Tour> Tours { get; set; }
    }
}
