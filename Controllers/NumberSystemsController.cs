using Microsoft.AspNetCore.Mvc;
using WebApiTranslationOfNumberSystems.Services;

namespace WebApiTranslationOfNumberSystems.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumberSystemsController : ControllerBase
    {
        [HttpGet("supported-bases")]
        public IActionResult GetSupportedBases()
        {
            var bases = new List<int> { 2, 8, 10, 16 };
            return Ok(new { supportedBases = bases });
        }

        [HttpPost("convert")]
        public IActionResult ConvertNumber([FromBody] ConvertRequest request)
        {
            var result = NumberSystemConverter.Convert(request.Value, request.FromBase, request.ToBase);
            return Ok(new ConversionResultMessage(result));
        }
    }
} 