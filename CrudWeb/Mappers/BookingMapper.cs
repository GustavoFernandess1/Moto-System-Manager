using System;
using CrudWeb.Models.Request;
using CrudWeb.Models.Response;

namespace CrudWeb.Mappers
{
    public class BookingMapper
    {
        public BookingMotorcycleRequest Map(BookingMotorcycleRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "O objeto BookingMotorcycleRequest não pode ser nulo.");
            return new BookingMotorcycleRequest
            {
                CourierId = request.CourierId,
                MotorcycleId = request.MotorcycleId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ExpectedEndDate = request.ExpectedEndDate,
                Plan = request.Plan
            };
        }

        public UpdateReturnDateRequest Map(UpdateReturnDateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "O objeto UpdateReturnDateRequest não pode ser nulo.");
            return new UpdateReturnDateRequest
            {
                ReturnDate = request.ReturnDate
            };
        }
    }
}