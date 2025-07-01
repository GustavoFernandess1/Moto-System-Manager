using System.Threading.Tasks;
using CrudWeb.Interfaces;
using CrudWeb.Models;
using CrudWeb.Models.Request;
using CrudWeb.Models.Response;
using CrudWeb.Repositories;
using CrudWeb.Validators;

namespace CrudWeb.Services
{

    public class CouriersService : ICouriersService
    {
        private readonly ICouriersRepository _couriersRepository;

        public CouriersService(ICouriersRepository couriersRepository)
        {
            _couriersRepository = couriersRepository;
        }

        public async Task<CouriersResponse> CreateCourierAsync(CouriersRequest request)
        {
            CouriersRequestValidator.Validate(request);

            var allCouriers = await _couriersRepository.GetAllAsync();
            if (allCouriers.Any(c => c.TaxId == request.TaxId))
                throw new InvalidOperationException("Já existe um entregador cadastrado com este CNPJ.");
            if (allCouriers.Any(c => c.LicenseNumber == request.LicenseNumber))
                throw new InvalidOperationException("Já existe um entregador cadastrado com este número de CNH.");

            var model = new CouriersModel
            {
                Identifier = request.Identifier,
                Name = request.Name,
                TaxId = request.TaxId.ToString(),
                Birthdate = request.Birthdate,
                LicenseNumber = request.LicenseNumber,
                LicenseType = request.LicenseType,
                LicenseImage = request.LicenseImage ?? string.Empty
            };

            await _couriersRepository.AddAsync(model);

            return new CouriersResponse
            {
                Identifier = model.Identifier,
                Name = model.Name,
                TaxId = model.TaxId,
                Birthdate = model.Birthdate,
                LicenseNumber = model.LicenseNumber,
                LicenseType = model.LicenseType,
                Message = "Entregador cadastrado com sucesso!"
            };
        }
    }
}