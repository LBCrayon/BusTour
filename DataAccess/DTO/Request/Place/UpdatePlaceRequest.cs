using System;
namespace DataAccess.DTO.Request.Place
{
	public class UpdatePlaceRequest
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? TimeStay { get; set; }
        public int? MediaId { get; set; }
    }
}

