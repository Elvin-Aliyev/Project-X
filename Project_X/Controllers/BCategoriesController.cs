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
    public class BCategoriesController : ControllerBase
    {
        private readonly DSEMLDbContext dbContext;
        private readonly IBCategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public BCategoriesController(DSEMLDbContext dbContext,IBCategoryRepository categoryRepository,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var category = await categoryRepository.GetAllAsync();
            //var spsDto = mapper.Map<List<SponsorsDto>>(sps);
            return Ok(category);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var cat = await categoryRepository.GetByIdAsync(id);
            if (cat == null)
            {
                return NotFound();
            }
            var catdto = mapper.Map<BCategoryDto>(cat);
            return Ok(catdto);
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] BCategoryDto catDto)
        {
            
            var cat = mapper.Map<BCategory>(catDto);

            await categoryRepository.CreateAsync(cat);

            var catDt = mapper.Map<BCategoryDto>(cat);
            return CreatedAtAction(nameof(Get), new { id = cat.Id }, catDt);

        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] BCategoryDto spDto)
        {
            var dbsp = mapper.Map<BCategory>(spDto);


            dbsp = await categoryRepository.UpdateAsync(id, dbsp);
            if (dbsp == null)
            {
                return NotFound();
            }

            var sDto = mapper.Map<BCategoryDto>(dbsp);

            return Ok(sDto);
            
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var dbcat = await categoryRepository.DeleteAsync(id);
            if (dbcat == null) return NotFound();
            var catdto = mapper.Map<BCategoryDto>(dbcat);
            return Ok(catdto);
        }

    }
}
