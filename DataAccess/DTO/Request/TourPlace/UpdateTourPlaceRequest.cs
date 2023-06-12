using System;
namespace DataAccess.DTO.Request.TourPlace
{
	public class UpdateTourPlaceRequest
	{
        public int Id { get; set; }
        public int? JourneyId { get; set; }
        public int? PlaceId { get; set; }
    }
}

