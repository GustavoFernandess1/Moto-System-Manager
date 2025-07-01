using System.Threading.Tasks;
using CrudWeb.Models.Request;
using CrudWeb.Models.Response;

namespace CrudWeb.Repositories
{
    public interface IBookingRepository
    {
        Task<int> AddBookingAsync(BookingMotorcycleRequest bookingRequest);
        Task<BookingResponse> GetBookingByIdAsync(int bookingId);
        Task UpdateReturnDateAsync(int bookingId, DateTime returnDate);
        Task<bool> HasActiveBookingForCourierAsync(string courierId);
        Task<bool> HasActiveBookingForMotorcycleAsync(string motorcycleId);
    }
}
