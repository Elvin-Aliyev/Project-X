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
    public class ContractsController : ControllerBase
    {
        private readonly DSEMLDbContext dbContext;
        private readonly IContactRepository contactRepository;
        private readonly IMapper mapper;
        public ContractsController(DSEMLDbContext dbContext, IContactRepository contactRepository, IMapper mapper)
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
        public async Task<IActionResult> Get([FromRoute] Guid id )
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
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] ContactDto contactdto)
        {

            var contact = mapper.Map<Contact>(contactdto);

            await contactRepository.CreateAsync(contact);

            var contDt = mapper.Map<ContactDto>(contact);

            return CreatedAtAction(nameof(Get), new { id = contact.Id }, contDt);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
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
