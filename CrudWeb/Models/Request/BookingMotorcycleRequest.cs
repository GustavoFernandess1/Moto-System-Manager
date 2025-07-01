using System;
using CrudWeb.Enums;

namespace CrudWeb.Models.Request
{
    /// <summary>
    /// Requisição para realizar o booking de uma motocicleta.
    /// </summary>
    public class BookingMotorcycleRequest
    {
        /// <summary>
        /// Identificador do entregador (courier) que está realizando a reserva.
        /// </summary>
        public string CourierId { get; set; }

        /// <summary>
        /// Identificador da motocicleta a ser reservada.
        /// </summary>
        public string MotorcycleId { get; set; }

        /// <summary>
        /// Data e hora de início da reserva.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Data e hora de término da reserva (preenchido apenas na devolução).
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Data e hora prevista para devolução da motocicleta.
        /// </summary>
        public DateTime ExpectedEndDate { get; set; }

        /// <summary>
        /// Plano de reserva selecionado (ex: Diário, Semanal, Mensal).
        /// </summary>
        public BookingPlan Plan { get; set; }
    }
}
