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
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IContactRepository contactRepository;
        private readonly IMapper mapper;
        public ContactController(AppDbContext dbContext, IContactRepository contactRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.contactRepository = contactRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await contactRepository.GetAllAsync();
            return Ok(contacts);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var contact = await contactRepository.GetByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            var contactdto = mapper.Map<ContactDto>(contact);
            return Ok(contactdto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactDto contactdto)
        {
            var contact = mapper.Map<Contact>(contactdto);
            contact.CreatedAt = DateTime.UtcNow;
            await contactRepository.CreateAsync(contact);
            var contDt = mapper.Map<ContactDto>(contact);
            return CreatedAtAction(nameof(Get), new { id = contact.Id }, contDt);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ContactDto spDto)
        {
            var dbsp = mapper.Map<Contact>(spDto);

            dbsp = await contactRepository.UpdateAsync(id, dbsp);
            if (dbsp == null)
            {
                return NotFound();
            }

            var sDto = mapper.Map<ContactDto>(dbsp);

            return Ok(sDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var dbcat = await contactRepository.DeleteAsync(id);
            if (dbcat == null) return NotFound();
            var catdto = mapper.Map<ContactDto>(dbcat);
            return Ok(catdto);
        }
    }
}
