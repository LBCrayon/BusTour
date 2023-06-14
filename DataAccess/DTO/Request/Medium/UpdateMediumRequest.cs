using System;
namespace DataAccess.DTO.Request.Medium
{
	public class UpdateMediumRequest
	{
        public string? Video { get; set; }
        public string? Music { get; set; }
        public string? Audio { get; set; }
        public string? ImgUrl { get; set; }
        public string? Language { get; set; }
        public string? Blog { get; set; }
    }
}

