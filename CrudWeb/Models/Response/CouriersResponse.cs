using System;
using CrudWeb.Enums;

namespace CrudWeb.Models.Response
{
    public class CouriersResponse
    {
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string TaxId { get; set; }
        public DateTime Birthdate { get; set; }
        public string LicenseNumber { get; set; }
        public LicenseType LicenseType { get; set; }
        public string Message { get; set; }
    }
}
