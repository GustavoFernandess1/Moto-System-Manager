using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudWeb.Interfaces;
using CrudWeb.Models.Request;
using CrudWeb.Models.Response;
using CrudWeb.Repositories;
using CrudWeb.Validators;

namespace CrudWeb.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingResponse> CreateBookingAsync(BookingMotorcycleRequest bookingRequest)
        {
            BookingValidator.Validate(bookingRequest);

            if (await _bookingRepository.HasActiveBookingForCourierAsync(bookingRequest.CourierId))
                throw new InvalidOperationException("Já existe uma locação ativa para este entregador.");
            if (await _bookingRepository.HasActiveBookingForMotorcycleAsync(bookingRequest.MotorcycleId))
                throw new InvalidOperationException("Já existe uma locação ativa para esta moto.");

            var bookingId = await _bookingRepository.AddBookingAsync(bookingRequest);

            return new BookingResponse
            {
                Id = bookingId,
                CourierId = bookingRequest.CourierId,
                MotorcycleId = bookingRequest.MotorcycleId,
                StartDate = bookingRequest.StartDate,
                EndDate = bookingRequest.EndDate,
                ExpectedEndDate = bookingRequest.ExpectedEndDate,
                Plan = bookingRequest.Plan,
                TotalValue = 0,
                Fine = null,
                Message = "Agendamento criado com sucesso."
            };
        }

        public async Task<BookingResponse> GetBookingByIdAsync(int bookingId)
        {
            BookingValidator.ValidateBookingId(bookingId);
            
            if (bookingId <= 0)
                throw new ArgumentException("O ID do agendamento deve ser maior que zero.", nameof(bookingId));

            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (booking == null)
                throw new KeyNotFoundException($"Agendamento com ID {bookingId} não encontrado.");

            return new BookingResponse
            {
                Id = booking.Id,
                CourierId = booking.CourierId,
                MotorcycleId = booking.MotorcycleId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                ExpectedEndDate = booking.ExpectedEndDate,
                Plan = booking.Plan,
                TotalValue = booking.TotalValue,
                Fine = booking.Fine,
                Message = "Agendamento encontrado com sucesso."
            };
        }

        public async Task<BookingResponse> UpdateReturnDateAsync(int bookingId, UpdateReturnDateRequest request)
        {
            BookingValidator.ValidateReturnDate(request);

            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (booking == null)
                throw new KeyNotFoundException($"Agendamento com ID {bookingId} não encontrado.");

            booking.EndDate = request.ReturnDate;
            
            await _bookingRepository.UpdateReturnDateAsync(bookingId, request.ReturnDate);

            return new BookingResponse
            {
                Id = booking.Id,
                CourierId = booking.CourierId,
                MotorcycleId = booking.MotorcycleId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                ExpectedEndDate = booking.ExpectedEndDate,
                Plan = booking.Plan,
                TotalValue = booking.TotalValue,
                Fine = booking.Fine,
                Message = "Data de retorno atualizada com sucesso."
            };
        }
    }
}