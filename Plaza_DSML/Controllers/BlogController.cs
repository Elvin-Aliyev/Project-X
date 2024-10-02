using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plaza_DSML.Models;
using Plaza_DSML.Models.DTOs;
using Plaza_DSML.Repositories;
using Plaza_DSML.Services;

namespace Plaza_DSML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController(IFileService fileService, IBlogRepository blogRepository, ILogger<BlogController> logger) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromForm] BlogDto blogtoadd)
        {
            try
            {
                if (blogtoadd.ImageFile?.Length > 1 * 2024 * 2024)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                }
                string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
                string createdImageName = await fileService.SaveFileAsync(blogtoadd.ImageFile, allowedFileExtentions);

                var blog = new Blog
                {
                    Content = blogtoadd.Content,
                    Title = blogtoadd.Title,
                    CreatedDate = DateTime.UtcNow,
                    IsPublished = false,
                    BlogImage = createdImageName
                };
                var createdProduct = await blogRepository.CreateAsync(blog);
                return CreatedAtAction(nameof(CreateBlog), createdProduct);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBlog([FromRoute] Guid id, [FromForm] UpdateBlogDto updateBlogRequest)
        {
            try
            {
                if (id != updateBlogRequest.Id)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"id in url and form body does not match.");
                }

                var existingProduct = await blogRepository.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Product with id: {id} does not found");
                }
                string oldImage = existingProduct.BlogImage;
                if (updateBlogRequest.ImageFile != null)
                {
                    if (updateBlogRequest.ImageFile?.Length > 1 * 1024 * 1024)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                    }
                    string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
                    string createdImageName = await fileService.SaveFileAsync(updateBlogRequest.ImageFile, allowedFileExtentions);
                    updateBlogRequest.BlogImage = createdImageName;
                }

                // mapping `ProductDTO` to `Product` manually. You can use automapper.
                existingProduct.Id = updateBlogRequest.Id;
                existingProduct.Title = updateBlogRequest.Title;
                existingProduct.IsPublished = updateBlogRequest.IsPublished;
                existingProduct.Content = updateBlogRequest.Content;
                existingProduct.BlogImage = updateBlogRequest.BlogImage;

                var updatedProduct = await blogRepository.UpdateAsync(existingProduct);

                // if image is updated, then we have to delete old image from directory
                if (updateBlogRequest.ImageFile != null)
                    fileService.DeleteFile(oldImage);

                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBlog([FromRoute] Guid id)
        {
            try
            {
                var existingProduct = await blogRepository.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Product with id: {id} does not found");
                }

                await blogRepository.DeleteAsync(existingProduct);
                // After deleting product from database,remove file from directory.
                fileService.DeleteFile(existingProduct.BlogImage);
                return NoContent();  // return 204
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogById([FromRoute]Guid id)
        {
            var product = await blogRepository.GetByIdAsync(id);
            if (product == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Product with id: {id} does not found");
            }
            return Ok(product);
        }
        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            var products = await blogRepository.GetAllAsync();
            return Ok(products);
        }
    }
}
