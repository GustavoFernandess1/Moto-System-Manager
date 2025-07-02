using CrudWeb.Interfaces;
using CrudWeb.Mappers;
using CrudWeb.Models;
using CrudWeb.Models.Request;
using CrudWeb.Models.Response;
using CrudWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrudWeb.Controllers
{
    [Route("[controller]")]
    public class CouriersController : Controller
    {
        private readonly ILogger<CouriersController> _logger;
        private readonly ICouriersService _couriersService;
        private readonly CouriersMapper _couriersMapper;

        public CouriersController(
            ILogger<CouriersController> logger,
            ICouriersService couriersService,
            CouriersMapper couriersMapper)
        {
            _logger = logger;
            _couriersService = couriersService;
            _couriersMapper = couriersMapper;
        }

        /// <summary>
        /// Cadastra um novo entregador (courier).
        /// </summary>
        /// <param name="couriersRequest">Dados do entregador a ser cadastrado</param>
        /// <returns>Dados do entregador cadastrado</returns>
        /// <response code="200">Entregador cadastrado com sucesso</response>
        /// <response code="400">Parâmetros inválidos</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(CouriersResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> RegisterCourier([FromBody] CouriersRequest couriersRequest)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Iniciando cadastro de entregador.", DateTime.Now, logIdentifier);

            CouriersRequest mappedRequest = _couriersMapper.Map(couriersRequest);
            _logger.LogInformation("{Time} {LogIdentifier} - CouriersRequest convertido para model.", DateTime.Now, logIdentifier);

            CouriersResponse result = await _couriersService.CreateCourierAsync(mappedRequest);
            _logger.LogInformation("{Time} {LogIdentifier} - Chamando serviço para cadastrar entregador.", DateTime.Now, logIdentifier);

            _logger.LogInformation("{Time} {LogIdentifier} - Entregador cadastrado com sucesso.", DateTime.Now, logIdentifier);
            return Ok(result);
        }

        /// <summary>
        /// Envia a foto da CNH do entregador.
        /// </summary>
        /// <param name="id">ID do entregador</param>
        /// <param name="request">Objeto contendo a imagem em base64</param>
        /// <returns>Status da operação</returns>
        /// <response code="201">Imagem salva com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost("{id}/LicenseImage")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadLicenseImage([FromRoute] string id, [FromBody] UploadLicenseImageRequest request)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - Iniciando upload da CNH do entregador {CourierId}.", DateTime.Now, logIdentifier, id);

            UploadLicenseImageRequest uploadImage = _couriersMapper.Map(request);
            _logger.LogInformation("{Time} {LogIdentifier} - UploadLicenseImageRequest convertido para model.", DateTime.Now, logIdentifier);

            UploadLicenseImageResponse result = await _couriersService.SaveLicenseImageAsync(id, uploadImage.LicenseImage);
            _logger.LogInformation("{Time} {LogIdentifier} - Chamando serviço para salvar imagem da CNH do entregador {CourierId}.", DateTime.Now, logIdentifier, id);

            _logger.LogInformation("{Time} {LogIdentifier} - Imagem da CNH do entregador {CourierId} salva com sucesso.", DateTime.Now, logIdentifier, id);
            return Ok(result);
        }
    }
}