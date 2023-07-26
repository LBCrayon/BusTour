using System;

namespace DataAccess.DTO.Request.Journey
{
    public class CreateJourneyRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<int> PlaceIds { get; set; }
    }
}