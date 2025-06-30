using CrudWeb.Models.Request;
using System;
using System.Collections.Generic;

namespace CrudWeb.Validators
{
    public static class CouriersRequestValidator
    {
        public static List<string> Validate(CouriersRequest request)
        {
            var errors = new List<string>();
            if (request == null)
            {
                errors.Add("Request cannot be null.");
                return errors;
            }
            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("Name is required.");
            var taxIdString = request.TaxId.ToString();
            if (string.IsNullOrWhiteSpace(taxIdString) || taxIdString.Length != 14)
                errors.Add("TaxId (CNPJ) is required and must be 14 characters.");
            if (request.Birthdate == DateTime.MinValue)
                errors.Add("BirthDate is required.");
            if (string.IsNullOrWhiteSpace(request.LicenseNumber))
                errors.Add("LicenseNumber is required.");
            if (request.LicenseType == default)
                errors.Add("LicenseType is required.");
            return errors;
        }
    }
}
