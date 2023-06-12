using System;
using BusinessObject.Models;

namespace DataAccess.DTO.Response
{
	public class PlaceResponse
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? TimeStay { get; set; }
        public int? MediaId { get; set; }
        public int? TimeLineId { get; set; }
    }
}

