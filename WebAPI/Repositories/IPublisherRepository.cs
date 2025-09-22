using WebAPI.Models.Domain;
using WebAPI.Models.DTO;
using WebAPI.Models.DTO;

namespace WebAPI.Repositories
{
    public interface IPublisherRepository
    {
        List<PublisherDTO> GetAllPublishers();
        PublisherNoIdDTO GetPublisherById(int id);
        PublisherDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO);
        PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO);
        Publishers? DeletePublisherById(int id);
    }
}
