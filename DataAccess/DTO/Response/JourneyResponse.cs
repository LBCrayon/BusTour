using System;
namespace DataAccess.DTO.Response
{
	public class JourneyResponse
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
    }
}

