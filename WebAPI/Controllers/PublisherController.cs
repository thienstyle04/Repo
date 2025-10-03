using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Domain;
using WebAPI.Models.DTO;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;
        private object _dbContext;

        public PublishersController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers()
        {
            var publishers = _publisherRepository.GetAllPublishers();
            return Ok(publishers);
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var publisher = _publisherRepository.GetPublisherById(id);
            if (publisher == null) return NotFound($"Publisher with Id {id} not found");
            return Ok(publisher);
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] AddPublisherRequestDTO addPublisherRequestDTO)
        {
            //var publisher = _publisherRepository.AddPublisher(addPublisherRequestDTO);
            //return Ok(publisher);
            if (ValidateAddPublisher(addPublisherRequestDTO))
            {
                var addPublisher = _publisherRepository.AddPublisher(addPublisherRequestDTO);
                return Ok(addPublisher);
            }
            else return BadRequest(ModelState);
        }

        [HttpPut("update-publisher-by-id/{id}")]
        public IActionResult UpdatePublisherById(int id, [FromBody] PublisherNoIdDTO publisherNoIdDTO)
        {
            var updated = _publisherRepository.UpdatePublisherById(id, publisherNoIdDTO);
            if (updated == null) return NotFound($"Publisher with Id {id} not found");
            return Ok(updated);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {

            try
            {
                var deleted = _publisherRepository.DeletePublisherById(id);
                if (deleted == null) return NotFound($"Publisher with Id {id} not found");
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }

        private bool ValidateAddPublisher(AddPublisherRequestDTO addPublisherRequestDTO)
        {
            if (addPublisherRequestDTO == null)
            {
                ModelState.AddModelError(nameof(addPublisherRequestDTO), "Please add publisher data");
                return false;
            }

            // Kiểm tra Name không được rỗng
            if (string.IsNullOrWhiteSpace(addPublisherRequestDTO.Name))
            {
                ModelState.AddModelError(nameof(addPublisherRequestDTO.Name), "Publisher name cannot be empty");
            }

            // Kiểm tra trùng lặp (không phân biệt hoa thường)
            var exists = _publisherRepository.GetAllPublishers()
                .Any(p => p.Name.ToLower() == addPublisherRequestDTO.Name.ToLower());

            if (exists)
            {
                ModelState.AddModelError(nameof(addPublisherRequestDTO.Name),
                    $"Publisher with name '{addPublisherRequestDTO.Name}' already exists");

            }
            // Nếu có lỗi thì return false
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        } 
        
    }
}
