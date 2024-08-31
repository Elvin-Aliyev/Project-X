using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_X.CustomActionFilters;
using Project_X.Data;
using Project_X.Models.Domain;
using Project_X.Models.DTO;
using Project_X.Repositories;

namespace Project_X.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqsController : ControllerBase
    {
        private readonly DSEMLDbContext dbContext;
        private readonly IFaqRepository faqRepository;
        private readonly IMapper mapper;
        public FaqsController(DSEMLDbContext dbContext, IFaqRepository faqRepository,IMapper mapper)
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


            /*var faqdto = new List<FaqsDto>();

            foreach (var faq in faqs)
            {
                faqdto.Add(new FaqsDto
                {
                    Id = faq.Id,
                    Answer = faq.Answer,
                    Question = faq.Question,
                });
            }*/
            var faqdto = mapper.Map<List<FaqsDto>>(faqs);


            return Ok(faqdto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            /*var faq = await dbContext.Faqs.FindAsync(id);*/
            var faq = await faqRepository.GetByIdAsync(id);


            if (faq == null)
            {
                return NotFound();
            }
            /*var faqdto = new FaqsDto();

            faqdto = new FaqsDto
            {
                Id = faq.Id,
                Answer = faq.Answer,
                Question = faq.Question,
            };*/
            var faqdto = mapper.Map<FaqsDto>(faq);
            return Ok(faqdto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddFaqRequestDto faqsDto)
        {
            /*var faq = new Faqs
        {
            Answer = faqsDto.Answer,
            Question = faqsDto.Question
        };*/
            var faq = mapper.Map<Faq>(faqsDto); //gelen dto nu faqs a chevirir

            /*await dbContext.Faqs.AddAsync(faq);
            await dbContext.SaveChangesAsync();*/
            await faqRepository.CreateAsync(faq);


            /*var faqdto = new FaqsDto
            {
                Id=faq.Id,
                Answer = faq.Answer,
                Question = faq.Question
            };*/
            var faqdto = mapper.Map<FaqsDto>(faq);

            return CreatedAtAction(nameof(Get), new { id = faq.Id }, faqdto);
            
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateFaqRequestDto updateFaqRequestDto)
        {
            //var dbfaq = await dbContext.Faqs.FirstOrDefaultAsync(x => x.Id == id);

            /*var dbfaq = new Faqs
            {
                Question = updateFaqRequestDto.Question,
                Answer = updateFaqRequestDto.Answer,
            };*/
            var dbfaq = mapper.Map<Faq>(updateFaqRequestDto);


            dbfaq = await faqRepository.UpdateAsync(id, dbfaq);
            if (dbfaq == null)
            {
                return NotFound();
            }

            /*dbfaq.Answer = updateFaqRequestDto.Answer;
            dbfaq.Question = updateFaqRequestDto.Question;

            await dbContext.SaveChangesAsync();*/

            /*var faqDto = new FaqsDto
            {
                Id = id,
                Answer= dbfaq.Answer,
                Question = dbfaq.Question
            };*/
            var faqDto = mapper.Map<FaqsDto>(updateFaqRequestDto);

            return Ok(faqDto);
            
            
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //var faq =await dbContext.Faqs.FirstOrDefaultAsync(x=>x.Id == id);
            var faq = await faqRepository.DeleteAsync(id);

            if(faq == null)
            {
                return NotFound();
            }
            /*dbContext.Remove(faq);
            await dbContext.SaveChangesAsync();*/

            /*var faqDto = new FaqsDto
            {
                Id = faq.Id,
                Question = faq.Question,
                Answer = faq.Answer
            };*/
            var faqDto = mapper.Map<FaqsDto>(faq);

            return Ok(faqDto);
        }
    
    
    }
}
