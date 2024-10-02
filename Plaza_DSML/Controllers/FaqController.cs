using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plaza_DSML.Data;
using Plaza_DSML.Models;
using Plaza_DSML.Models.DTOs;
using Plaza_DSML.Repositories;

namespace Plaza_DSML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IFaqRepository faqRepository;
        private readonly IMapper mapper;
        public FaqController(AppDbContext dbContext,IFaqRepository faqRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.faqRepository = faqRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            /*var faqs = await dbContext.Faqs.ToListAsync();*/
            var faqs = await faqRepository.GetAllAsync();
            //var faqdto = mapper.Map<List<FaqsDto>>(faqs);
            return Ok(faqs);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var faq = await faqRepository.GetByIdAsync(id);

            if (faq == null)
            {
                return NotFound();
            }
            var faqdto = mapper.Map<FaqDto>(faq);
            return Ok(faqdto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FaqDto faqsDto)
        {
            var faq = mapper.Map<Faq>(faqsDto);

            await faqRepository.CreateAsync(faq);

            var faqdto = mapper.Map<FaqDto>(faq);

            return CreatedAtAction(nameof(Get), new { id = faq.Id }, faqdto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] FaqDto updateFaqRequestDto)
        {
            var dbfaq = mapper.Map<Faq>(updateFaqRequestDto);

            dbfaq = await faqRepository.UpdateAsync(id, dbfaq);
            if (dbfaq == null)
            {
                return NotFound();
            }

            var faqDto = mapper.Map<FaqDto>(updateFaqRequestDto);

            return Ok(faqDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var faq = await faqRepository.DeleteAsync(id);

            if (faq == null)
            {
                return NotFound();
            }
            var faqDto = mapper.Map<FaqDto>(faq);

            return Ok(faqDto);
        }
    }
}
