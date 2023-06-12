using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Place
    {
        public Place()
        {
            TourPlaces = new HashSet<TourPlace>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? TimeStay { get; set; }
        public int? MediaId { get; set; }
        public int? TimeLineId { get; set; }

        public virtual Medium? Media { get; set; }
        public virtual ICollection<TourPlace> TourPlaces { get; set; }
    }
}
