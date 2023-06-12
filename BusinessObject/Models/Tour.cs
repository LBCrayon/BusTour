using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Tour
    {
        public Tour()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
        public int? BusId { get; set; }
        public int? JourneyId { get; set; }
        public int? SurchargeId { get; set; }

        public virtual Bus? Bus { get; set; }
        public virtual Journey? Journey { get; set; }
        public virtual Surcharge? Surcharge { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
