using System;
namespace DataAccess.DTO.Request.Class
{
	public class CreateClassRequest
	{
        public string Name { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public int Status { get; set; }
    }
}

