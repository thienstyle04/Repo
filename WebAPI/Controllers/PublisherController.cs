using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Domain;
using WebAPI.Models.DTO;
using WebAPI.Models.DTO;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;

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
            var publisher = _publisherRepository.AddPublisher(addPublisherRequestDTO);
            return Ok(publisher);
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
            var deleted = _publisherRepository.DeletePublisherById(id);
            if (deleted == null) return NotFound($"Publisher with Id {id} not found");
            return Ok(deleted);
        }
    }
}
