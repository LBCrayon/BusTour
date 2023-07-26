using System;
namespace DataAccess.DTO.Request.Tour
{
	public class UpdateTourRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? TotalPrice { get; set; }
        public double? UnitPPrice { get; set; }
        public string? Description { get; set; }
        public int? VehicleId { get; set; }
        public int? JourneyId { get; set; }
        public int? SurchargeId { get; set; }
    }
}

    