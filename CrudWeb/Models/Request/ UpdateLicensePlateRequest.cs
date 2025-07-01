using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudWeb.Models.Request
{
    public class UpdateLicensePlateRequest
    {
        /// <summary>
        /// Placa da moto a ser atualizada.
        /// /// </summary>
        public string LicensePlate { get; set; }
    }
}