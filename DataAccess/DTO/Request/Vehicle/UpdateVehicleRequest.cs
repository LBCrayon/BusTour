using System;
namespace DataAccess.DTO.Request.Vehicle
{
	public class UpdateVehicleRequest
	{
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? TotalSeat { get; set; }
        public int? Status { get; set; }
    }
}

