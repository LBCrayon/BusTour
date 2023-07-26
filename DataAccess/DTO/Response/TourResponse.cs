using System;
using BusinessObject.Models;

namespace DataAccess.DTO.Response
{
    public class TourResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? UnitPrice { get; set; }
        public double? TotalPrice { get; set; }
        public string? Description { get; set; }
        public int? VehicleId { get; set; }
        public int? JourneyId { get; set; }
        public int? SurchargeId { get; set; }

        public VehicleResponse? Vehicle { get; set; }
        public JourneyResponse? Journey { get; set; }
        public SurchargeResponse? Surcharge { get; set; }
    }
}