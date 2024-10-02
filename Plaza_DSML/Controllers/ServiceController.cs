using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plaza_DSML.Models.DTOs;
using Plaza_DSML.Models;
using Plaza_DSML.Repositories;
using Plaza_DSML.Services;

namespace Plaza_DSML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController(IFileService fileService, IServiceRepository serviceRepository, ILogger<ServiceController> logger) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateService([FromForm] ServiceDto servicetoadd)
        {
            try
            {
                if (servicetoadd.ImageFile?.Length > 1 * 2024 * 2024)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                }
                string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
                string createdImageName = await fileService.SaveFileAsync(servicetoadd.ImageFile, allowedFileExtentions);

                var service = new Service
                {
                    Content = servicetoadd.Content,
                    Title = servicetoadd.Title,
                    CreatedDate = DateTime.UtcNow,
                    IsPublished = false,
                    ServiceImage = createdImageName
                };
                var createdProduct = await serviceRepository.CreateAsync(service);
                return CreatedAtAction(nameof(CreateService), createdProduct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateService([FromRoute] Guid id, [FromForm] UpdateServiceDto updateServiceRequest)
        {
            try
            {
                if (id != updateServiceRequest.Id)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"id in url and form body does not match.");
                }

                var existingProduct = await serviceRepository.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Product with id: {id} does not found");
                }
                string oldImage = existingProduct.ServiceImage;
                if (updateServiceRequest.ImageFile != null)
                {
                    if (updateServiceRequest.ImageFile?.Length > 1 * 1024 * 1024)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                    }
                    string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
                    string createdImageName = await fileService.SaveFileAsync(updateServiceRequest.ImageFile, allowedFileExtentions);
                    updateServiceRequest.ServiceImage = createdImageName;
                }

                // mapping `ProductDTO` to `Product` manually. You can use automapper.
                existingProduct.Id = updateServiceRequest.Id;
                existingProduct.Title = updateServiceRequest.Title;
                existingProduct.IsPublished = updateServiceRequest.IsPublished;
                existingProduct.Content = updateServiceRequest.Content;
                existingProduct.ServiceImage = updateServiceRequest.ServiceImage;

                var updatedProduct = await serviceRepository.UpdateAsync(existingProduct);

                // if image is updated, then we have to delete old image from directory
                if (updateServiceRequest.ImageFile != null)
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
        public async Task<IActionResult> DeleteService([FromRoute] Guid id)
        {
            try
            {
                var existingProduct = await serviceRepository.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Product with id: {id} does not found");
                }

                await serviceRepository.DeleteAsync(existingProduct);
                // After deleting product from database,remove file from directory.
                fileService.DeleteFile(existingProduct.ServiceImage);
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
        public async Task<IActionResult> GetBlogById([FromRoute] Guid id)
        {
            var product = await serviceRepository.GetByIdAsync(id);
            if (product == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Product with id: {id} does not found");
            }
            return Ok(product);
        }
        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            var products = await serviceRepository.GetAllAsync();
            return Ok(products);
        }
    }
}
