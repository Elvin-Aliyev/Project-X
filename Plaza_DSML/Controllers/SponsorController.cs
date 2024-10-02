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
    public class SponsorController(IFileService fileService, ISponsorRepository sponsorRepository, ILogger<SponsorController> logger) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateSponsor([FromForm] SponsorDto sponsortoadd)
        {
            try
            {
                if (sponsortoadd.ImageFile?.Length > 1 * 2024 * 2024)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                }
                string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
                string createdImageName = await fileService.SaveFileAsync(sponsortoadd.ImageFile, allowedFileExtentions);

                var sponsor = new Sponsor
                {
                    Name = sponsortoadd.Name,
                    ImageUrl = createdImageName
                };
                var createdProduct = await sponsorRepository.CreateAsync(sponsor);
                return CreatedAtAction(nameof(CreateSponsor), createdProduct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateSponsor([FromRoute] Guid id, [FromForm] UpdateSponsorDto updateSponsorRequest)
        {
            try
            {
                if (id != updateSponsorRequest.Id)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"id in url and form body does not match.");
                }

                var existingProduct = await sponsorRepository.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Product with id: {id} does not found");
                }
                string oldImage = existingProduct.ImageUrl;
                if (updateSponsorRequest.ImageFile != null)
                {
                    if (updateSponsorRequest.ImageFile?.Length > 1 * 1024 * 1024)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                    }
                    string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
                    string createdImageName = await fileService.SaveFileAsync(updateSponsorRequest.ImageFile, allowedFileExtentions);
                    updateSponsorRequest.ImageUrl = createdImageName;
                }

                // mapping `ProductDTO` to `Product` manually. You can use automapper.
                existingProduct.Id = updateSponsorRequest.Id;
                existingProduct.Name = updateSponsorRequest.Name;
                existingProduct.ImageUrl = updateSponsorRequest.ImageUrl;

                var updatedProduct = await sponsorRepository.UpdateAsync(existingProduct);

                // if image is updated, then we have to delete old image from directory
                if (updateSponsorRequest.ImageFile != null)
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
        public async Task<IActionResult> DeleteSponsor([FromRoute] Guid id)
        {
            try
            {
                var existingProduct = await sponsorRepository.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Product with id: {id} does not found");
                }

                await sponsorRepository.DeleteAsync(existingProduct);
                // After deleting product from database,remove file from directory.
                fileService.DeleteFile(existingProduct.ImageUrl);
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
        public async Task<IActionResult> GetSponsorById([FromRoute] Guid id)
        {
            var product = await sponsorRepository.GetByIdAsync(id);
            if (product == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Product with id: {id} does not found");
            }
            return Ok(product);
        }
        [HttpGet]
        public async Task<IActionResult> GetSponsors()
        {
            var products = await sponsorRepository.GetAllAsync();
            return Ok(products);
        }
    }
}
