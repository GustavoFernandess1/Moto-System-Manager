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
                errors.Add("Request cannot be null.");
                return errors;
            }
            if (string.IsNullOrWhiteSpace(request.Identifier))
                errors.Add("Identifier is required.");
            if (request.Year < 1900 || request.Year > DateTime.Now.Year + 1)
                errors.Add("Year is required and must be a valid year.");
            if (string.IsNullOrWhiteSpace(request.Model))
                errors.Add("Model is required.");
            if (string.IsNullOrWhiteSpace(request.LicensePlate))
                errors.Add("LicensePlate is required.");
            return errors;
        }

        public static void Validate(string licensePlate)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
                throw new ArgumentException("License plate cannot be null or empty.", nameof(licensePlate));
            if (licensePlate.Length < 7 || licensePlate.Length > 8)
                throw new ArgumentException("License plate must be between 7 and 8 characters long.", nameof(licensePlate));
        }

        public static void Validate(UpdateLicensePlateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "UpdateLicensePlateRequest cannot be null.");
            if (string.IsNullOrWhiteSpace(request.LicensePlate))
                throw new ArgumentException("License plate cannot be null or empty.", nameof(request.LicensePlate));
            if (request.LicensePlate.Length < 7 || request.LicensePlate.Length > 8)
                throw new ArgumentException("License plate must be between 7 and 8 characters long.", nameof(request.LicensePlate));
        }
    }
}