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

namespace CrudWeb.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public MotorcycleService(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }
        public async Task<MotorcycleResponse> RegisterMotorcycle(MotorcycleRegisterRequest request)
        {
            MotorcycleRequestValidator.Validate(request);

            var existing = await _motorcycleRepository.GetByLicensePlateAsync(request.LicensePlate);
            if (existing != null)
                throw new InvalidOperationException("Já existe uma moto cadastrada com esta placa.");

            var model = new MotorcycleModel
            {
                Identifier = request.Identifier,
                Model = request.Model,
                Year = (int)request.Year,
                LicensePlate = request.LicensePlate
            };

            await _motorcycleRepository.AddMotorcycleAsync(model);

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
            MotorcycleRequestValidator.Validate(licensePlate);

            var model = await _motorcycleRepository.GetByLicensePlateAsync(licensePlate);

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
            var existing = await _motorcycleRepository.GetByLicensePlateAsync(licensePlate);
            if (existing != null && existing.Identifier != (await _motorcycleRepository.GetByIdAsync(id))?.Identifier)
                throw new InvalidOperationException("Já existe uma moto cadastrada com esta placa.");

            var model = await _motorcycleRepository.GetByIdAsync(id);
            if (model == null)
            {
                throw new Exception("Moto não encontrada.");
            }

            await _motorcycleRepository.UpdateLicensePlateAsync(id, licensePlate);
            model.LicensePlate = licensePlate;

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

            var model = await _motorcycleRepository.GetByIdAsync(id);
            if (model == null)
            {
                throw new Exception("Moto não encontrada.");
            }

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
            var model = await _motorcycleRepository.GetByIdAsync(id);
            if (model == null)
            {
                throw new Exception("Moto não encontrada.");
            }

            await _motorcycleRepository.DeleteMotorcycleAsync(id);

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