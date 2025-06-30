using System;
using CrudWeb.Enums;

namespace CrudWeb.Models
{
    public class CouriersModel
    {
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string TaxId { get; set; }
        public DateTime Birthdate { get; set; }
        public string LicenseNumber { get; set; }
        public LicenseType LicenseType { get; set; }
        public string LicenseImage { get; set; }
    }
}
