using System;
using BusinessObject.Models;

namespace DataAccess.DTO.Response
{
    public class JourneyResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<PlaceResponse> Places { get; set; }
    }
}