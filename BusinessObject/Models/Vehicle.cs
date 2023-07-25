using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? TotalSeat { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Tour> Tours { get; set; }
    }
}
