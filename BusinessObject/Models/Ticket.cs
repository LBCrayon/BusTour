using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public double? TotalPrice { get; set; }
        public int? AvailableTicket { get; set; }
        public int? TotalTicket { get; set; }
        public string? Departure { get; set; }
        public string? Arrival { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? TourId { get; set; }
        public int? ClassId { get; set; }

        public virtual Class? Class { get; set; }
        public virtual Tour? Tour { get; set; }
    }
}
