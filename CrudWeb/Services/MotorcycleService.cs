using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudWeb.Interfaces;
using CrudWeb.Models;
using CrudWeb.Models.Request;
using CrudWeb.Models.Response;
using CrudWeb.Repositories;
using CrudWeb.Validators;
using Microsoft.Extensions.Logging;

namespace CrudWeb.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly ILogger<MotorcycleService> _logger;

        public MotorcycleService(IMotorcycleRepository motorcycleRepository, ILogger<MotorcycleService> logger)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
        }

        public async Task<MotorcycleResponse> RegisterMotorcycle(MotorcycleRegisterRequest request)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Iniciando registro de moto.", DateTime.Now, logIdentifier);

            MotorcycleRequestValidator.Validate(request);
            _logger.LogInformation("{Time} {LogIdentifier} - Dados da moto validados.", DateTime.Now, logIdentifier);

            var existing = await _motorcycleRepository.GetByLicensePlateAsync(request.LicensePlate);
            if (existing != null)
            {
                _logger.LogWarning("{Time} {LogIdentifier} - Já existe uma moto cadastrada com esta placa: {LicensePlate}.", DateTime.Now, logIdentifier, request.LicensePlate);
                throw new InvalidOperationException("Já existe uma moto cadastrada com esta placa.");
            }

            var model = new MotorcycleModel
            {
                Identifier = request.Identifier,
                Model = request.Model,
                Year = (int)request.Year,
                LicensePlate = request.LicensePlate
            };

            await _motorcycleRepository.AddMotorcycleAsync(model);
            _logger.LogInformation("{Time} {LogIdentifier} - Moto registrada com sucesso. Id: {MotorcycleId}", DateTime.Now, logIdentifier, model.Identifier);

            return new MotorcycleResponse
            {
                Identifier = model.Identifier,
                Model = model.Model,
                Year = model.Year,
                LicensePlate = model.LicensePlate,
                Message = "Moto registrada com sucesso."
            };
        }

        public async Task<MotorcycleResponse> GetByLicensePlateAsync(string licensePlate)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Buscando moto por placa.", DateTime.Now, logIdentifier);

            MotorcycleRequestValidator.Validate(licensePlate);
            _logger.LogInformation("{Time} {LogIdentifier} - Placa validada.", DateTime.Now, logIdentifier);

            var model = await _motorcycleRepository.GetByLicensePlateAsync(licensePlate);
            if (model == null)
            {
                _logger.LogWarning("{Time} {LogIdentifier} - Moto com placa {LicensePlate} não encontrada.", DateTime.Now, logIdentifier, licensePlate);
                throw new Exception("Moto não encontrada.");
            }

            _logger.LogInformation("{Time} {LogIdentifier} - Moto encontrada com sucesso.", DateTime.Now, logIdentifier);
            return new MotorcycleResponse
            {
                Identifier = model.Identifier,
                Model = model.Model,
                Year = model.Year,
                LicensePlate = model.LicensePlate,
                Message = "Moto encontrada com sucesso."
            };
        }

        public async Task<MotorcycleResponse> UpdateLicensePlateAsync(int id, string licensePlate)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Iniciando atualização de placa da moto.", DateTime.Now, logIdentifier);

            var existing = await _motorcycleRepository.GetByLicensePlateAsync(licensePlate);
            if (existing != null && existing.Identifier != (await _motorcycleRepository.GetByIdAsync(id))?.Identifier)
            {
                _logger.LogWarning("{Time} {LogIdentifier} - Já existe uma moto cadastrada com esta placa: {LicensePlate}.", DateTime.Now, logIdentifier, licensePlate);
                throw new InvalidOperationException("Já existe uma moto cadastrada com esta placa.");
            }

            var model = await _motorcycleRepository.GetByIdAsync(id);
            if (model == null)
            {
                _logger.LogWarning("{Time} {LogIdentifier} - Moto com id {MotorcycleId} não encontrada.", DateTime.Now, logIdentifier, id);
                throw new Exception("Moto não encontrada.");
            }

            await _motorcycleRepository.UpdateLicensePlateAsync(id, licensePlate);
            model.LicensePlate = licensePlate;
            _logger.LogInformation("{Time} {LogIdentifier} - Placa da moto atualizada com sucesso. Id: {MotorcycleId}", DateTime.Now, logIdentifier, model.Identifier);

            return new MotorcycleResponse
            {
                Identifier = model.Identifier,
                Model = model.Model,
                Year = model.Year,
                LicensePlate = model.LicensePlate,
                Message = "Placa da moto atualizada com sucesso."
            };
        }

        public async Task<MotorcycleResponse> GetByIdAsync(int id)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Buscando moto por id.", DateTime.Now, logIdentifier);

            var model = await _motorcycleRepository.GetByIdAsync(id);
            if (model == null)
            {
                _logger.LogWarning("{Time} {LogIdentifier} - Moto com id {MotorcycleId} não encontrada.", DateTime.Now, logIdentifier, id);
                throw new Exception("Moto não encontrada.");
            }

            _logger.LogInformation("{Time} {LogIdentifier} - Moto encontrada com sucesso.", DateTime.Now, logIdentifier);
            return new MotorcycleResponse
            {
                Identifier = model.Identifier,
                Model = model.Model,
                Year = model.Year,
                LicensePlate = model.LicensePlate,
                Message = "Moto encontrada com sucesso."
            };
        }

        public async Task<MotorcycleResponse> DeleteMotorcycleAsync(int id)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Iniciando exclusão de moto.", DateTime.Now, logIdentifier);

            var model = await _motorcycleRepository.GetByIdAsync(id);
            if (model == null)
            {
                _logger.LogWarning("{Time} {LogIdentifier} - Moto com id {MotorcycleId} não encontrada.", DateTime.Now, logIdentifier, id);
                throw new Exception("Moto não encontrada.");
            }

            await _motorcycleRepository.DeleteMotorcycleAsync(id);
            _logger.LogInformation("{Time} {LogIdentifier} - Moto deletada com sucesso. Id: {MotorcycleId}", DateTime.Now, logIdentifier, model.Identifier);

            return new MotorcycleResponse
            {
                Identifier = model.Identifier,
                Model = model.Model,
                Year = model.Year,
                LicensePlate = model.LicensePlate,
                Message = "Moto deletada com sucesso."
            };
        }
    }
}