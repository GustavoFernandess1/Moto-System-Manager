using System;
using System.Collections.Generic;
using CrudWeb.Enums;
using CrudWeb.Models.Request;

namespace CrudWeb.Validators
{
    public static class BookingValidator
    {
        public static List<string> Validate(BookingMotorcycleRequest request)
        {
            var errors = new List<string>();
            if (request == null)
            {
                errors.Add("A requisição não pode ser nula.");
                return errors;
            }
            if (string.IsNullOrWhiteSpace(request.CourierId))
                errors.Add("O campo CourierId é obrigatório.");
            if (string.IsNullOrWhiteSpace(request.MotorcycleId))
                errors.Add("O campo MotorcycleId é obrigatório.");
            if (request.StartDate == default)
                errors.Add("O campo StartDate é obrigatório.");
            if (request.EndDate == default)
                errors.Add("O campo EndDate é obrigatório.");
            if (request.ExpectedEndDate == default)
                errors.Add("O campo ExpectedEndDate é obrigatório.");
            if (!Enum.IsDefined(typeof(BookingPlan), request.Plan))
                errors.Add("O campo Plan deve ser um valor válido.");
            if (request.StartDate >= request.EndDate)
                errors.Add("A data de início deve ser anterior à data de término.");
            if (request.StartDate >= request.ExpectedEndDate)
                errors.Add("A data de início deve ser anterior à data de previsão de término.");
            if (request.EndDate > request.ExpectedEndDate.AddDays(30))
                errors.Add("A data de término está muito distante da data de previsão de término.");
            return errors;
        }

        public static void ValidateBookingId(int bookingId)
        {
            if (bookingId <= 0)
                throw new ArgumentException("O ID do agendamento deve ser maior que zero.", nameof(bookingId));
        }

        public static void ValidateReturnDate(UpdateReturnDateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "O objeto UpdateReturnDateRequest não pode ser nulo.");
            if (request.ReturnDate == default)
                throw new ArgumentException("A data de retorno é obrigatória.", nameof(request.ReturnDate));
            if (request.ReturnDate <= DateTime.Now)
                throw new ArgumentException("A data de retorno deve ser uma data futura.", nameof(request.ReturnDate));
        }
    }
}