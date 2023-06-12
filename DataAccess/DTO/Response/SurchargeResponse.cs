using System;
namespace DataAccess.DTO.Response
{
	public class SurchargeResponse
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public double? Fee { get; set; }
    }
}

