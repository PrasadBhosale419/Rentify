using AutoMapper;
using Backend.Dtos;
using Backend.Interfaces;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using WebAPI.Models;

namespace Backend.Controllers
{
    public class PropertyController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IPhotoService photoService;

        public PropertyController(IUnitOfWork uow, IMapper mapper, IPhotoService photoService)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.photoService = photoService;
        }

        [HttpGet("list/{sellRent}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyList(int sellRent)
        {
            var properties = await uow.propertyRepository.GetPropertiesAsync(sellRent);
            var propertyListDTO = mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return Ok(propertyListDTO);
        }

        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<IActionResult> AddProperty(PropertyDto propertyDto)
        {
            var property = mapper.Map<Property>(propertyDto);
            var userId = GetUserId();
            property.PostedBy = userId;
            property.LastUpdatedBy = userId;
            uow.propertyRepository.AddProperty(property);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpPost("add/photo/{propId}")]
        [AllowAnonymous] 
        public async Task<IActionResult> AddPropertyPhoto(IFormFile file, int propId) 
        {
            var result = await photoService.UploadPhotoAsync(file);
            if (result.Error != null)
                return BadRequest(result.Error.Message);
            var userId = GetUserId();

            var property = await uow.propertyRepository.GetPropertyByIdAsync(propId);

            if (property.PostedBy != userId)
                return BadRequest("You are not authorised to upload photo for this property");

            var photo = new Photo
            {
                ImageUrl = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            if (property.Photos.Count == 0)
            {
                photo.IsPrimary = true;
            }

            property.Photos.Add(photo);
            if (await uow.SaveAsync())
            {
                var photoDto = mapper.Map<PhotoDto>(photo);
                return Ok(photoDto); // Returns a 200 OK response with the DTO as the body
            }

            return BadRequest("Some problem occured in uploading photo..");
        }

        [HttpPost("set-primary-photo/{propId}/{photoPublicId}")]
        [AllowAnonymous]
        public async Task<IActionResult> SetPrimaryPhoto(int propId, string photoPublicId)
        {
            var userId = GetUserId();

            var property = await uow.propertyRepository.GetPropertyByIdAsync(propId);

            if (property.PostedBy != userId)
                return BadRequest("You are not authorised to change the photo");

            if (property == null || property.PostedBy != userId)
                return BadRequest("No such property or photo exists");

            var photo = property.Photos.FirstOrDefault(p => p.PublicId == photoPublicId);

            if (photo == null)
                return BadRequest("No such property or photo exists");

            if (photo.IsPrimary)
                return BadRequest("This is already a primary photo");


            var currentPrimary = property.Photos.FirstOrDefault(p => p.IsPrimary);
            if (currentPrimary != null) currentPrimary.IsPrimary = false;
            photo.IsPrimary = true;

            if (await uow.SaveAsync()) return NoContent();

            return BadRequest("Failed to set primary photo");
        }

        [HttpDelete("delete-photo/{propId}/{photoPublicId}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeletePhoto(int propId, string photoPublicId)
        {
            var userId = GetUserId();

            var property = await uow.propertyRepository.GetPropertyByIdAsync(propId);

            if (property.PostedBy != userId)
                return BadRequest("You are not authorised to delete the photo");

            if (property == null || property.PostedBy != userId)
                return BadRequest("No such property or photo exists");

            var photo = property.Photos.FirstOrDefault(p => p.PublicId == photoPublicId);

            if (photo == null)
                return BadRequest("No such property or photo exists");

            if (photo.IsPrimary)
                return BadRequest("You can not delete primary photo");

            if (photo.PublicId != null)
            {
                var result = await photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            property.Photos.Remove(photo);

            if (await uow.SaveAsync()) return Ok();

            return BadRequest("Failed to delete photo");
        }


        [HttpGet("detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            var property = await uow.propertyRepository.GetPropertyDetailAsync(id);
            var propertyDTO = mapper.Map<PropertyDetailDto>(property);
            return Ok(propertyDTO);
        }
    }
}
