using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        private readonly IRegionRepository _regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(IRegionRepository region,
                                IMapper mapper,
                                ILogger<RegionsController> logger)
        {
            this._regionRepository = region;
            this.mapper = mapper;
            this._logger = logger;
        }



        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {



            throw new Exception("An error occurred while getting all regions");

            _logger.LogInformation("Getting all regions");

            //Recuperar los datos de la BD
            var regions = await _regionRepository.GetAllRegionsAsync();

            var regionsDto = mapper.Map<IEnumerable<RegionDto>>(regions);

            //Return Dto
            _logger.LogInformation($"All Regions data: {JsonSerializer.Serialize(regionsDto)}");

            return Ok(regionsDto);


        }


        [HttpGet]
        [Route("{regionId:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegion([FromRoute] Guid regionId)
        {

            var region = await _regionRepository.GetRegionAsync(regionId);


            if (region == null)
                return NotFound();

            var regionDto = mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }


        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto regionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                //Convertir el Dto en un modelo
                var region = mapper.Map<Region>(regionDto);
                region.Id = Guid.NewGuid();


                //Crear un nuevo objeto de tipo Region
                await _regionRepository.CreateRegionAsync(region);

                var regionResponseDto = mapper.Map<RegionDto>(region);

                return CreatedAtAction(nameof(GetRegion), new { regionId = region.Id }, regionResponseDto);
            }



        }

        [HttpPut]
        [Route("{regionId:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid regionId, [FromBody] UpdateRegionRequestDto regionDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {

                //Map Dto to Domain Model
                var region = mapper.Map<Region>(regionDto);

                await _regionRepository.UpdateRegionAsync(region);

                var RegionDto = mapper.Map(region, regionDto);

                return Ok(regionDto);
            }

        }


        [HttpDelete]
        [Route("{regionId:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid regionId)
        {


            var eliminado = await _regionRepository.DeleteRegionAsync(regionId);

            if (eliminado == null)
            {
                return NotFound();
            }

            return Ok();

        }

    }
}
