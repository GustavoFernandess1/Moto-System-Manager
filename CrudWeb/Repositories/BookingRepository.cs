using System.Threading.Tasks;
using System.Data;
using CrudWeb.Models.Request;
using CrudWeb.Repositories;
using Dapper;
using CrudWeb.Models.Response;

namespace CrudWeb.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly Infraestructure.DapperContext _context;

        public BookingRepository(Infraestructure.DapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddBookingAsync(BookingMotorcycleRequest bookingRequest)
        {
            var query = @"INSERT INTO bookings (courier_id, motorcycle_id, start_date, end_date, expected_end_date, plan)
                          VALUES (@CourierId, @MotorcycleId, @StartDate, @EndDate, @ExpectedEndDate, @Plan)
                          RETURNING id;";
            using (IDbConnection connection = _context.CreateConnection())
            {
                var id = await connection.ExecuteScalarAsync<int>(query, bookingRequest);
                return id;
            }
        }

        public async Task<BookingResponse> GetBookingByIdAsync(int bookingId)
        {
            var query = "SELECT * FROM bookings WHERE id = @Id";
            using (IDbConnection connection = _context.CreateConnection())
            {
                var booking = await connection.QueryFirstOrDefaultAsync<BookingResponse>(query, new { Id = bookingId });
                return booking;
            }
        }

        public async Task UpdateReturnDateAsync(int bookingId, DateTime returnDate)
        {
            var query = "UPDATE bookings SET end_date = @ReturnDate WHERE id = @BookingId";
            using (IDbConnection connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { ReturnDate = returnDate, BookingId = bookingId });
            }
        }

        public async Task<bool> HasActiveBookingForCourierAsync(string courierId)
        {
            var query = @"SELECT COUNT(1) FROM bookings WHERE courier_id = @courierId AND end_date IS NULL";
            using (IDbConnection connection = _context.CreateConnection())
            {
                var count = await connection.ExecuteScalarAsync<int>(query, new { courierId });
                return count > 0;
            }
        }

        public async Task<bool> HasActiveBookingForMotorcycleAsync(string motorcycleId)
        {
            var query = @"SELECT COUNT(1) FROM bookings WHERE motorcycle_id = @motorcycleId AND end_date IS NULL";
            using (IDbConnection connection = _context.CreateConnection())
            {
                var count = await connection.ExecuteScalarAsync<int>(query, new { motorcycleId });
                return count > 0;
            }
        }
    }
}
