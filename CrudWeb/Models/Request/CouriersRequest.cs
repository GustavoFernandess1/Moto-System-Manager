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
        /// <summary>
        /// Identificador único do entregador.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Nome completo do entregador.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// CPF do entregador.
        /// </summary>
        public string TaxId { get; set; }

        /// <summary>
        /// Data de nascimento do entregador.
        /// </summary>
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// Número da CNH do entregador.
        /// </summary>
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Tipo de habilitação do entregador.
        /// </summary>
        public LicenseType LicenseType { get; set; }

        /// <summary>
        /// Imagem da CNH do entregador (base64 ou URL).
        /// </summary>
        public string LicenseImage { get; set; }
    }
}