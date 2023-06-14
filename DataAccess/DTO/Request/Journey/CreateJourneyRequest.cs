using System;
namespace DataAccess.DTO.Request.Journey
{
	public class CreateJourneyRequest
	{
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}

