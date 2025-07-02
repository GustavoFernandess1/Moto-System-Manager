using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudWeb.Interfaces;
using CrudWeb.Models.Request;
using CrudWeb.Models.Response;
using CrudWeb.Repositories;
using CrudWeb.Validators;
using Microsoft.Extensions.Logging;

namespace CrudWeb.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ILogger<BookingService> _logger;

        public BookingService(IBookingRepository bookingRepository, ILogger<BookingService> logger)
        {
            _bookingRepository = bookingRepository;
            _logger = logger;
        }

        public async Task<BookingResponse> CreateBookingAsync(BookingMotorcycleRequest bookingRequest)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Iniciando criação de agendamento.", DateTime.Now, logIdentifier);

            BookingValidator.Validate(bookingRequest);
            _logger.LogInformation("{Time} {LogIdentifier} - Dados do agendamento validados.", DateTime.Now, logIdentifier);

            if (await _bookingRepository.HasActiveBookingForCourierAsync(bookingRequest.CourierId))
            {
                _logger.LogWarning("{Time} {LogIdentifier} - Já existe uma locação ativa para o entregador {CourierId}.", DateTime.Now, logIdentifier, bookingRequest.CourierId);
                throw new InvalidOperationException("Já existe uma locação ativa para este entregador.");
            }
            if (await _bookingRepository.HasActiveBookingForMotorcycleAsync(bookingRequest.MotorcycleId))
            {
                _logger.LogWarning("{Time} {LogIdentifier} - Já existe uma locação ativa para a moto {MotorcycleId}.", DateTime.Now, logIdentifier, bookingRequest.MotorcycleId);
                throw new InvalidOperationException("Já existe uma locação ativa para esta moto.");
            }

            var bookingId = await _bookingRepository.AddBookingAsync(bookingRequest);
            _logger.LogInformation("{Time} {LogIdentifier} - Agendamento criado com sucesso. Id: {BookingId}", DateTime.Now, logIdentifier, bookingId);

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
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Buscando agendamento por ID.", DateTime.Now, logIdentifier);

            BookingValidator.ValidateBookingId(bookingId);
            _logger.LogInformation("{Time} {LogIdentifier} - ID do agendamento validado.", DateTime.Now, logIdentifier);

            if (bookingId <= 0)
            {
                _logger.LogWarning("{Time} {LogIdentifier} - O ID do agendamento deve ser maior que zero.", DateTime.Now, logIdentifier);
                throw new ArgumentException("O ID do agendamento deve ser maior que zero.", nameof(bookingId));
            }

            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (booking == null)
            {
                _logger.LogWarning("{Time} {LogIdentifier} - Agendamento com ID {BookingId} não encontrado.", DateTime.Now, logIdentifier, bookingId);
                throw new KeyNotFoundException($"Agendamento com ID {bookingId} não encontrado.");
            }

            _logger.LogInformation("{Time} {LogIdentifier} - Agendamento encontrado com sucesso.", DateTime.Now, logIdentifier);
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
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Iniciando atualização da data de retorno do agendamento.", DateTime.Now, logIdentifier);

            BookingValidator.ValidateReturnDate(request);
            _logger.LogInformation("{Time} {LogIdentifier} - Dados de retorno validados.", DateTime.Now, logIdentifier);

            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (booking == null)
            {
                _logger.LogWarning("{Time} {LogIdentifier} - Agendamento com ID {BookingId} não encontrado.", DateTime.Now, logIdentifier, bookingId);
                throw new KeyNotFoundException($"Agendamento com ID {bookingId} não encontrado.");
            }

            booking.EndDate = request.ReturnDate;
            var diasPlano = (int)booking.Plan;
            var valorDiaria = booking.Plan switch
            {
                Enums.BookingPlan.PlanSevenDays => 30m,
                Enums.BookingPlan.PlanFifteenDays => 28m,
                Enums.BookingPlan.PlanThirtyDays => 22m,
                Enums.BookingPlan.PlanFortyFiveDays => 20m,
                Enums.BookingPlan.PlanFiftyDays => 18m,
                _ => 30m
            };
            var diasUtilizados = (booking.EndDate - booking.StartDate).Days;
            if (diasUtilizados < 1) diasUtilizados = 1;
            decimal total = 0;
            decimal? multa = null;

            if (booking.EndDate <= booking.ExpectedEndDate)
            {
                total = diasUtilizados * valorDiaria;
                if (booking.EndDate < booking.ExpectedEndDate)
                {
                    var diasNaoUsados = (booking.ExpectedEndDate - booking.EndDate).Days;
                    decimal percentualMulta = booking.Plan switch
                    {
                        Enums.BookingPlan.PlanSevenDays => 0.20m,
                        Enums.BookingPlan.PlanFifteenDays => 0.40m,
                        _ => 0m
                    };
                    multa = diasNaoUsados * valorDiaria * percentualMulta;
                    total += multa ?? 0;
                }
            }
            else
            {
                var diasAtraso = (booking.EndDate - booking.ExpectedEndDate).Days;
                total = diasPlano * valorDiaria + (diasAtraso * 50m);
                multa = diasAtraso * 50m;
            }

            booking.TotalValue = total;
            booking.Fine = multa;

            await _bookingRepository.UpdateReturnDateAsync(bookingId, request.ReturnDate);
            _logger.LogInformation("{Time} {LogIdentifier} - Data de retorno do agendamento atualizada. Valor total: {TotalValue}, Multa: {Fine}", DateTime.Now, logIdentifier, booking.TotalValue, booking.Fine);

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
                Message = "Data de retorno atualizada com sucesso. Valor e multa calculados."
            };
        }
    }
}