using System;
namespace DataAccess.DTO.Response
{
    public class ClassResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public double? Amount { get; set; }
        public int? Status { get; set; }
    }
}

