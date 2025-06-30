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
    public class MotorcycleController : Controller
    {
        private readonly ILogger<MotorcycleController> _logger;
        private readonly MotorcycleMapper _motorcycleMapper;
        private readonly IMotorcycleService _motorcycleService;

        public MotorcycleController(ILogger<MotorcycleController> logger, IMotorcycleService motorcycleService, MotorcycleMapper motorcycleMapper)
        {
            _logger = logger;
            _motorcycleService = motorcycleService;
            _motorcycleMapper = motorcycleMapper;
        }


        [HttpPost("[action]")]
        [ProducesResponseType(typeof(MotorcycleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegisterMotorcycle([FromBody] MotorcycleRegisterRequest motorcycleRequest)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - MotorcycleServiceRequest mapped.", DateTime.Now, logIdentifier);

            _ = _motorcycleMapper.Map(motorcycleRequest);
            _logger.LogInformation("{Time} {LogIdentifier} - MotorcycleServiceRequest converted to model.", DateTime.Now, logIdentifier);

            MotorcycleResponse result = await _motorcycleService.RegisterMotorcycle(motorcycleRequest);
            _logger.LogInformation("{Time} {LogIdentifier} - Motorcycle registered successfully.", DateTime.Now, logIdentifier);
            return Ok(result);
        }

        [HttpGet("ByLicensePlate/{licensePlate}")]
        [ProducesResponseType(typeof(MotorcycleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByLicensePlate(string licensePlate)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Requisição GetByLicensePlate recebida.", DateTime.Now, logIdentifier);

            var mappedRequest = new MotorcycleRegisterRequest { LicensePlate = licensePlate };
            _ = _motorcycleMapper.Map(mappedRequest);
            _logger.LogInformation("{Time} {LogIdentifier} - Requisição GetByLicensePlate mapeada.", DateTime.Now, logIdentifier);

            var result = await _motorcycleService.GetByLicensePlateAsync(licensePlate);

            _logger.LogInformation("{Time} {LogIdentifier} - Moto encontrada para a placa: {LicensePlate}.", DateTime.Now, logIdentifier, licensePlate);
            return Ok(result);
        }

        [HttpPut("ById/{id}")]
        [ProducesResponseType(typeof(MotorcycleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePlateMotorcycle([FromRoute] int id, [FromBody] UpdateLicensePlateRequest licensePlateRequest)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Requisição UpdatePlateMotorcycle recebida.", DateTime.Now, logIdentifier);

            UpdateLicensePlateRequest request = _motorcycleMapper.Map(licensePlateRequest);
            _logger.LogInformation("{Time} {LogIdentifier} - Requisição UpdatePlateMotorcycle mapeada.", DateTime.Now, logIdentifier);

            MotorcycleResponse result = await _motorcycleService.UpdateLicensePlateAsync(id, request.LicensePlate);
            _logger.LogInformation("{Time} {LogIdentifier} - Placa da moto atualizada com sucesso.", DateTime.Now, logIdentifier);
            return Ok(result);
        }

        [HttpGet("ById/{id}")]
        [ProducesResponseType(typeof(MotorcycleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotorcycleResponse>> GetById([FromRoute] int id)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Requisição GetById recebida.", DateTime.Now, logIdentifier);

            var result = await _motorcycleService.GetByIdAsync(id);
            _logger.LogInformation("{Time} {LogIdentifier} - Resultado retornado do serviço para o ID: {Id}.", DateTime.Now, logIdentifier, id);
            return Ok(result);
        }

        [HttpDelete("ById/{id}")]
        [ProducesResponseType(typeof(MotorcycleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotorcycleResponse>> DeleteMotorcycle([FromRoute] int id)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Requisição DeleteMotorcycle recebida.", DateTime.Now, logIdentifier);

            var result = await _motorcycleService.DeleteMotorcycleAsync(id);
            _logger.LogInformation("{Time} {LogIdentifier} - Resultado da exclusão retornado do serviço.", DateTime.Now, logIdentifier);
            return Ok(result);
        }
    } 
}