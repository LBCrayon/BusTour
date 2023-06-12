using System;
namespace DataAccess.DTO.Request.Surcharge
{
	public class UpdateSurchargeRequest
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public double? Fee { get; set; }
    }
}

