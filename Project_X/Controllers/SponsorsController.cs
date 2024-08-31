using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_X.CustomActionFilters;
using Project_X.Data;
using Project_X.Models.Domain;
using Project_X.Models.DTO;
using Project_X.Repositories;

namespace Project_X.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SponsorsController : ControllerBase
    {
        private readonly DSEMLDbContext dbContext;
        private readonly ISponsorRepository sponsorRepository;
        private readonly IMapper mapper;
        public SponsorsController(DSEMLDbContext dbContext,ISponsorRepository sponsorRepository,IMapper mapper)
        {
                this.dbContext = dbContext;
                this.sponsorRepository = sponsorRepository;
                this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sps = await sponsorRepository.GetAllAsync();
            //var spsDto = mapper.Map<List<SponsorsDto>>(sps);
            return Ok(sps);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var sp = await sponsorRepository.GetByIdAsync(id);


            if (sp == null)
            {
                return NotFound();
            }
            var spdto = mapper.Map<SponsorsDto>(sp);
            return Ok(spdto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] SponsorsDto spDto)
        {
            
            var sp = mapper.Map<Sponsors>(spDto); //gelen dto nu faqs a chevirir

            await sponsorRepository.CreateAsync(sp);

            var spdto = mapper.Map<SponsorsDto>(sp);

            return CreatedAtAction(nameof(Get), new { id = sp.Id }, spdto);
            
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] SponsorsDto spDto)
        {
                var dbsp = mapper.Map<Sponsors>(spDto);


                dbsp = await sponsorRepository.UpdateAsync(id, dbsp);
                if (dbsp == null)
                {
                    return NotFound();
                }

                var sDto = mapper.Map<SponsorsDto>(spDto);

                return Ok(sDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var sp = await sponsorRepository.DeleteAsync(id);

            if (sp == null)
            {
                return NotFound();
            }
            var spDto = mapper.Map<SponsorsDto>(sp);

            return Ok(spDto);
        }
    }
}
