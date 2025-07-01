using System;
using CrudWeb.Enums;

namespace CrudWeb.Models.Response
{
    public class BookingResponse
    {
        public int Id { get; set; }
        public string CourierId { get; set; }
        public string MotorcycleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public BookingPlan Plan { get; set; }
        public decimal TotalValue { get; set; }
        public decimal? Fine { get; set; }
        public string Message { get; set; }
    }
}
