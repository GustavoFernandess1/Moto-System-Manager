using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using CrudWeb.Enums;

namespace CrudWeb.Models.Request
{
    public class CouriersRequest
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