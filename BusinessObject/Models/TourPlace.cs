using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class TourPlace
    {
        public int Id { get; set; }
        public int? JourneyId { get; set; }
        public int? PlaceId { get; set; }

        public virtual Journey? Journey { get; set; }
        public virtual Place? Place { get; set; }
    }
}
