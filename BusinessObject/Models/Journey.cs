using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Journey
    {
        public Journey()
        {
            TourPlaces = new HashSet<TourPlace>();
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<TourPlace> TourPlaces { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
