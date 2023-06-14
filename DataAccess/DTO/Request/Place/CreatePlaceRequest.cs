using System;
namespace DataAccess.DTO.Request.Place
{
	public class CreatePlaceRequest
	{
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TimeStay { get; set; }
        public int MediaId { get; set; }
    }
}

