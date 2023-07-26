using System;
namespace DataAccess.DTO.Request.Ticket
{ 
	public class CreateTicketRequest
	{
		public double? UnitPrice { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public string? Departure { get; set; }
		public string? Arrival { get; set; }
		public int TourId { get; set; }
		public int ClassId { get; set; }
	}
}

