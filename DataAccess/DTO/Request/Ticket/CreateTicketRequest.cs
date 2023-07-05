using System;
namespace DataAccess.DTO.Request.Ticket
{
	public class CreateTicketRequest
	{
		public int TourId { get; set; }
		public string? Departure { get; set; }
		public string? Arrival { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
	

    }
}

