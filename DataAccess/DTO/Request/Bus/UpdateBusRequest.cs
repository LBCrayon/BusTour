﻿using System;
namespace DataAccess.DTO.Request.Bus
{
	public class UpdateBusRequest
	{
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? TotalSeat { get; set; }
    }
}

