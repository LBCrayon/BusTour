using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Medium
    {
        public Medium()
        {
            Places = new HashSet<Place>();
        }

        public int Id { get; set; }
        public string? Video { get; set; }
        public string? Music { get; set; }
        public string? Audio { get; set; }
        public string? ImgUrl { get; set; }
        public string? Language { get; set; }
        public string? Blog { get; set; }

        public virtual ICollection<Place> Places { get; set; }
    }
}
