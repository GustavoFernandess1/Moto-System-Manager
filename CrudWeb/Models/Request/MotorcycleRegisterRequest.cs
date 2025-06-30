using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace CrudWeb.Models.Request
{
    public class MotorcycleRegisterRequest
    {
        public string Identifier { get; set; }
        public decimal Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        
    }
}