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

    public class CouriersService : ICouriersService
    {
        private readonly ICouriersRepository _couriersRepository;
        private readonly ILogger<CouriersService> _logger;

        public CouriersService(ICouriersRepository couriersRepository, ILogger<CouriersService> logger)
        {
            _couriersRepository = couriersRepository;
            _logger = logger;
        }

        public async Task<CouriersResponse> CreateCourierAsync(CouriersRequest request)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Iniciando cadastro de entregador.", DateTime.Now, logIdentifier);

            CouriersRequestValidator.Validate(request);
            _logger.LogInformation("{Time} {LogIdentifier} - Dados do entregador validados.", DateTime.Now, logIdentifier);

            var allCouriers = await _couriersRepository.GetAllAsync();
            if (allCouriers.Any(c => c.TaxId == request.TaxId))
            {
                _logger.LogWarning("{Time} {LogIdentifier} - Já existe entregador com este CNPJ: {TaxId}.", DateTime.Now, logIdentifier, request.TaxId);
                throw new InvalidOperationException("Já existe um entregador cadastrado com este CNPJ.");
            }
            if (allCouriers.Any(c => c.LicenseNumber == request.LicenseNumber))
            {
                _logger.LogWarning("{Time} {LogIdentifier} - Já existe entregador com este número de CNH: {LicenseNumber}.", DateTime.Now, logIdentifier, request.LicenseNumber);
                throw new InvalidOperationException("Já existe um entregador cadastrado com este número de CNH.");
            }

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
            _logger.LogInformation("{Time} {LogIdentifier} - Entregador cadastrado com sucesso. Id: {CourierId}", DateTime.Now, logIdentifier, model.Identifier);

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

        public async Task<UploadLicenseImageResponse> SaveLicenseImageAsync(string courierId, string base64Image)
        {
            CouriersRequestValidator.Validate(base64Image);
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Iniciando upload da CNH para entregador {CourierId}.", DateTime.Now, logIdentifier, courierId);

            if (string.IsNullOrWhiteSpace(base64Image))
            {
                _logger.LogWarning("{Time} {LogIdentifier} - Imagem não informada para o entregador {CourierId}.", DateTime.Now, logIdentifier, courierId);
                return new UploadLicenseImageResponse { Sucess = false, Message = "Imagem não informada" };
            }

            byte[] imageBytes;
            try
            {
                imageBytes = Convert.FromBase64String(base64Image);
            }
            catch
            {
                _logger.LogWarning("{Time} {LogIdentifier} - Base64 inválido para o entregador {CourierId}.", DateTime.Now, logIdentifier, courierId);
                return new UploadLicenseImageResponse { Sucess = false, Message = "Imagem em base64 inválida" };
            }

            bool isPng = imageBytes.Length > 8 && imageBytes[0] == 0x89 && imageBytes[1] == 0x50 && imageBytes[2] == 0x4E && imageBytes[3] == 0x47;
            bool isBmp = imageBytes.Length > 2 && imageBytes[0] == 0x42 && imageBytes[1] == 0x4D;
            if (!isPng && !isBmp)
            {
                _logger.LogWarning("{Time} {LogIdentifier} - Formato inválido para o entregador {CourierId}.", DateTime.Now, logIdentifier, courierId);
                return new UploadLicenseImageResponse { Sucess = false, Message = "Formato de imagem não suportado. Apenas PNG ou BMP." };
            }

            string ext = isPng ? "png" : "bmp";
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "storage", "cnhs");
            Directory.CreateDirectory(folder);
            string filePath = Path.Combine(folder, $"{courierId}.{ext}");

            try
            {
                await File.WriteAllBytesAsync(filePath, imageBytes);
                _logger.LogInformation("{Time} {LogIdentifier} - Imagem da CNH do entregador {CourierId} salva em {FilePath}.", DateTime.Now, logIdentifier, courierId, filePath);
                return new UploadLicenseImageResponse { Sucess = true, Message = "Imagem salva com Sucess" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Time} {LogIdentifier} - Erro ao salvar imagem da CNH do entregador {CourierId}: {ErrorMessage}", DateTime.Now, logIdentifier, courierId, ex.Message);
                return new UploadLicenseImageResponse { Sucess = false, Message = "Erro ao salvar imagem" };
            }
        }
    }
}