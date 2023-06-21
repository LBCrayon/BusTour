namespace DataAccess.DTO.Request.Class
{
    public class UpdateClassRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public double? Amount { get; set; }
        public int? Status { get; set; }
    }
}