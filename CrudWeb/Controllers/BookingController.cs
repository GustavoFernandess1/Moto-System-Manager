using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CrudWeb.Interfaces;
using CrudWeb.Mappers;
using CrudWeb.Models.Request;
using CrudWeb.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CrudWeb.Controllers
{
    [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingService _bookingService;
        private readonly BookingMapper _bookingMapper;

        public BookingController(ILogger<BookingController> logger, IBookingService bookingService, BookingMapper bookingMapper)
        {
            _logger = logger;
            _bookingService = bookingService;
            _bookingMapper = bookingMapper;
        }

        /// <summary>
        /// Cria um novo agendamento de locação de moto.
        /// </summary>
        /// <param name="bookingRequest">Dados para criação do agendamento</param>
        /// <returns>Dados do agendamento criado</returns>
        /// <response code="201">Agendamento criado com sucesso</response>
        /// <response code="400">Parâmetros inválidos</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBooking([FromBody] BookingMotorcycleRequest bookingRequest)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Requisição de agendamento recebida.", DateTime.Now, logIdentifier);

            BookingMotorcycleRequest mappedRequest = _bookingMapper.Map(bookingRequest);
            _logger.LogInformation("{Time} {LogIdentifier} - BookingMotorcycleRequest convertido para model.", DateTime.Now, logIdentifier);

            var result = await _bookingService.CreateBookingAsync(mappedRequest);
            _logger.LogInformation("{Time} {LogIdentifier} - Agendamento criado com sucesso.", DateTime.Now, logIdentifier);
            return CreatedAtAction(nameof(CreateBooking), new { id = result.Id }, result);
        }

        /// <summary>
        /// Retorna os dados de um agendamento de locação pelo ID.
        /// </summary>
        /// <param name="id">ID do agendamento</param>
        /// <returns>Dados do agendamento</returns>
        /// <response code="200">Agendamento encontrado com sucesso</response>
        /// <response code="404">Agendamento não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookingById([FromRoute] int id)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Requisição de busca de agendamento por ID recebida.", DateTime.Now, logIdentifier);

            var result = await _bookingService.GetBookingByIdAsync(id);

            _logger.LogInformation("{Time} {LogIdentifier} - Agendamento encontrado para o ID: {Id}.", DateTime.Now, logIdentifier, id);
            return Ok(result);
        }

        /// <summary>
        /// Informa a data de devolução da moto e calcula o valor total da locação.
        /// </summary>
        /// <param name="id">ID do agendamento</param>
        /// <param name="updateRequest">Dados da devolução</param>
        /// <returns>Dados do agendamento atualizado</returns>
        /// <response code="200">Data de devolução informada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPut("{id}/Return")]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateReturnDate([FromRoute] int id, [FromBody] UpdateReturnDateRequest updateRequest)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Requisição de devolução recebida para o agendamento {Id}.", DateTime.Now, logIdentifier, id);

            UpdateReturnDateRequest mappedRequest = _bookingMapper.Map(updateRequest);
            _logger.LogInformation("{Time} {LogIdentifier} - UpdateReturnDateRequest convertido para model.", DateTime.Now, logIdentifier);

            var result = await _bookingService.UpdateReturnDateAsync(id, mappedRequest);
            _logger.LogInformation("{Time} {LogIdentifier} - Devolução processada para o agendamento {Id}.", DateTime.Now, logIdentifier, id);
            return Ok(result);
        }
    }
}