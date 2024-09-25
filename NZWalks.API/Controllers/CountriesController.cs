using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class CountriesController : ControllerBase
    {

        [MapToApiVersion("1.0")]
        [HttpGet]
        public IActionResult GetV1()
        {

            List<string> paises = new List<string>() { "España", "Italia", "Francia", "Alemania", "Portugal" };

            return Ok(paises);
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        public IActionResult GetV2()
        {

            List<string> paises = new List<string>() { "España", "Italia", "Francia", "Alemania", "Portugal" };
            int index = 1;
            List<CountryDto> countries = new List<CountryDto>();

            foreach (var country in paises)
            {
                countries.Add(new CountryDto() { CountryName = country, Id = index });
                index++;
            }

            return Ok(countries);
        }

    }


}
