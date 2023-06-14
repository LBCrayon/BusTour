using System;
namespace DataAccess.DTO.Request.Ticket
{
	public class CreateTicketRequest
	{
		public double? TotalPrice { get; set; }
		public bool? AvailableTicket { get; set; }
		public int? TotalTicket { get; set; }
		public string? Departure { get; set; }
		public string? Arrival { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public int? TourId { get; set; }

    }
}

