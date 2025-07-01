using CrudWeb.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudWeb.Validators
{
    public static class MotorcycleRequestValidator
    {
        public static List<string> Validate(MotorcycleRegisterRequest request)
        {
            var errors = new List<string>();
            if (request == null)
            {
                errors.Add("A requisição não pode ser nula.");
                return errors;
            }
            if (string.IsNullOrWhiteSpace(request.Identifier))
                errors.Add("O campo Identificador é obrigatório.");
            if (request.Year < 1900 || request.Year > DateTime.Now.Year + 1)
                errors.Add("O campo Ano é obrigatório e deve ser um ano válido.");
            if (string.IsNullOrWhiteSpace(request.Model))
                errors.Add("O campo Modelo é obrigatório.");
            if (string.IsNullOrWhiteSpace(request.LicensePlate))
                errors.Add("O campo Placa é obrigatório.");
            return errors;
        }

        public static void Validate(string licensePlate)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
                throw new ArgumentException("A placa não pode ser nula ou vazia.", nameof(licensePlate));
            if (licensePlate.Length < 7 || licensePlate.Length > 8)
                throw new ArgumentException("A placa deve ter entre 7 e 8 caracteres.", nameof(licensePlate));
        }

        public static void Validate(UpdateLicensePlateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "O objeto UpdateLicensePlateRequest não pode ser nulo.");
            if (string.IsNullOrWhiteSpace(request.LicensePlate))
                throw new ArgumentException("A placa não pode ser nula ou vazia.", nameof(request.LicensePlate));
            if (request.LicensePlate.Length < 7 || request.LicensePlate.Length > 8)
                throw new ArgumentException("A placa deve ter entre 7 e 8 caracteres.", nameof(request.LicensePlate));
        }
    }
}