using System;
using BusinessObject.Models;

namespace DataAccess.DTO.Response
{
	public class TourResponse
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
        public int? VehicleId { get; set; }
        public int? JourneyId { get; set; }
        public int? SurchargeId { get; set; }

        public virtual VehicleResponse? Vehicle { get; set; }
        public virtual ClassResponse? Journey { get; set; }
        public virtual SurchargeResponse? Surcharge { get; set; }
    }
}

