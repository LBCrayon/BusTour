using System;
namespace DataAccess.DTO.Request.Tour
{
	public class CreateTourRequest
	{
        public string? Name { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
        public int? VehicleId { get; set; }
        public int? JourneyId { get; set; }
        public int? SurchargeId { get; set; }

    }
}

