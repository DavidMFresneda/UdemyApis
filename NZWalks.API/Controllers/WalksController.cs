using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this._mapper = mapper;
            this._walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var walk = _mapper.Map<Walk>(addWalkRequestDto);
            walk.Id = Guid.NewGuid();

            var result = await _walkRepository.CreateWalkAsync(walk);

            var walkDto = _mapper.Map<WalkDto>(walk);

            return CreatedAtAction(nameof(GetWalk), new { walkId = walk.Id }, walkDto);

        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
                                                [FromQuery] string? sortBy, [FromQuery] bool isAscending,
                                                [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 4)
        {

            var walks = await _walkRepository.GetAllWalksAsync(filterOn, filterQuery, pageNumber, pageSize);

            var walksDto = _mapper.Map<IEnumerable<WalkDto>>(walks);

            return Ok(walksDto);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalk([FromRoute] Guid id)
        {
            var walk = await _walkRepository.GetWalkAsync(id);

            if (walk == null)
            {
                return NotFound();
            }

            var walkDto = _mapper.Map<WalkDto>(walk);

            return Ok(walkDto);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequest)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Mapeamos el dto a un objeto de dominio
            var walkUpdate = _mapper.Map<Walk>(updateWalkRequest);


            //Actualizamos el elemento
            var walkUpdated = await _walkRepository.UpdateWalkAsync(id, walkUpdate);

            if (walkUpdated != null)
                return Ok(_mapper.Map<WalkDto>(walkUpdated));
            else
                return NotFound();

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            var walk = _walkRepository.DeleteWalkAsync(id);

            if (walk == null)
                return NotFound();
            else
                return Ok(_mapper.Map<WalkDto>(walk));

        }

    }
}
