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
                errors.Add("A requisição não pode ser nula.");
                return errors;
            }
            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("O campo Nome é obrigatório.");
            var taxIdString = request.TaxId.ToString();
            if (string.IsNullOrWhiteSpace(taxIdString) || taxIdString.Length != 14)
                errors.Add("O campo TaxId (CNPJ) é obrigatório e deve ter 14 caracteres.");
            if (request.Birthdate == DateTime.MinValue)
                errors.Add("O campo Data de Nascimento é obrigatório.");
            if (string.IsNullOrWhiteSpace(request.LicenseNumber))
                errors.Add("O campo Número da CNH é obrigatório.");
            if (request.LicenseType == default)
                errors.Add("O campo Tipo de CNH é obrigatório.");
            return errors;
        }

        public static void Validate(string base64Image)
        {
            if (string.IsNullOrWhiteSpace(base64Image))
                throw new ArgumentException("A imagem da CNH não pode ser nula ou vazia.", nameof(base64Image));

            try
            {
                Convert.FromBase64String(base64Image);
            }
            catch (FormatException)
            {
                throw new ArgumentException("A imagem em base64 é inválida.", nameof(base64Image));
            }
        }
    }
}
