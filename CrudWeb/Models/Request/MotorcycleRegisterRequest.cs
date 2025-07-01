using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace CrudWeb.Models.Request
{
    public class MotorcycleRegisterRequest
    {
        /// <summary>
        /// Identificador único da motocicleta.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Ano de fabricação da motocicleta.
        /// </summary>
        public decimal Year { get; set; }

        /// <summary>
        /// Modelo da motocicleta.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Placa da motocicleta.
        /// </summary>
        public string LicensePlate { get; set; }
    }
}