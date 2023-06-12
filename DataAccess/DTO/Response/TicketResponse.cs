using System;
namespace DataAccess.DTO.Response
{
	public class TicketResponse
	{
        public int Id { get; set; }
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

