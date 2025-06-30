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

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(CouriersResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterCourier([FromBody] CouriersRequest couriersRequest)
        {
            string logIdentifier = Guid.NewGuid().ToString();
            _logger.LogInformation("{Time} {LogIdentifier} - CouriersServiceRequest mapeado.", DateTime.Now, logIdentifier);

            _ = _couriersMapper.Map(couriersRequest);
            _logger.LogInformation("{Time} {LogIdentifier} - CouriersServiceRequest convertido para model.", DateTime.Now, logIdentifier);

            CouriersResponse result = await _couriersService.CreateCourierAsync(couriersRequest);
            _logger.LogInformation("{Time} {LogIdentifier} - Entregador cadastrado com sucesso.", DateTime.Now, logIdentifier);

            return Ok(result);
        }
    }
}