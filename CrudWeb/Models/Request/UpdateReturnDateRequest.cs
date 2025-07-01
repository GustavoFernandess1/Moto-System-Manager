using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudWeb.Models.Request
{
    public class UpdateReturnDateRequest
    {
        /// <summary>
        /// Data e hora efetiva da devolução da motocicleta.
        /// </summary>
        public DateTime ReturnDate { get; set; }
    }
}