using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderProyectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : Controller
    {
        private readonly DollarApiService _dollarService;

        public CurrencyController(DollarApiService dollarService)
        {
            _dollarService = dollarService;
        }

        [HttpGet("dollar")]
        public async Task<IActionResult> GetDollar()
        {
            var dollar = await _dollarService.GetOfficialDollarRateAsync();
            if (dollar == null)
                return StatusCode(503, "No se pudo obtener la cotización del dólar oficial.");

            return Ok(dollar);
        }
    }
}
