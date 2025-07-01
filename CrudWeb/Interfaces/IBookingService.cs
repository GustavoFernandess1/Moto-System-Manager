using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudWeb.Models.Request;
using CrudWeb.Models.Response;

namespace CrudWeb.Interfaces
{
    public interface IBookingService
    {
        public Task<BookingResponse> CreateBookingAsync(BookingMotorcycleRequest bookingRequest);
        public Task<BookingResponse> GetBookingByIdAsync(int bookingId);
        public Task<BookingResponse> UpdateReturnDateAsync(int bookingId, UpdateReturnDateRequest request);
    }
}