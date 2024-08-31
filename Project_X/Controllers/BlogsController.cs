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
    public class BlogsController : ControllerBase
    {
        private readonly DSEMLDbContext dbContext;
        private readonly IBlogRepository blogRepository;
        private readonly IMapper mapper;
        public BlogsController(DSEMLDbContext dbContext, IBlogRepository blogRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.blogRepository = blogRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blog = await blogRepository.GetAllAsync();
            //var spsDto = mapper.Map<List<SponsorsDto>>(sps);
            return Ok(blog);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var blog = await blogRepository.GetByIdAsync(id);
            if (blog == null) return NotFound();
            var blogdto = mapper.Map<BlogDto>(blog);
            return Ok(blogdto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddBlogRequestDto addblogDto)
        {
            
            var blog = mapper.Map<Blog>(addblogDto);
            blog.CreatedAt = DateTime.UtcNow;
            await blogRepository.CreateAsync(blog);
            var blogDto = mapper.Map<BlogDto>(blog);
            return Ok(blogDto);
            
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBlogRequestDto updateBlogRequest)
        {
            var blog = mapper.Map<Blog>(updateBlogRequest);
            blog = await blogRepository.UpdateAsync(id, blog);
            if (blog == null) return NotFound();
            var blogdto = mapper.Map<BlogDto>(blog);
            return Ok(blogdto);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var dbblog = await blogRepository.DeleteAsync(id);
            if (dbblog == null) return NotFound();
            var blogDto = mapper.Map<BlogDto>(dbblog);
            return Ok(blogDto);
        }
    }
}
