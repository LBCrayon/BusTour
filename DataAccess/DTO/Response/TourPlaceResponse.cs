﻿using System;
using BusinessObject.Models;

namespace DataAccess.DTO.Response
{
	public class TourPlaceResponse
	{
        public int Id { get; set; }
        public int? JourneyId { get; set; }
        public int? PlaceId { get; set; }

        public virtual Journey? Journey { get; set; }
        public virtual Place? Place { get; set; }
    }
}

