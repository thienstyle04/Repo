using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models.Domain;
using WebAPI.Models.DTO;

namespace WebAPI.Repositories
{
    public class SQLPublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _dbContext;
        public SQLPublisherRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PublisherDTO> GetAllPublishers()
        {
            return _dbContext.Publishers.Select(p => new PublisherDTO
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }

        public PublisherNoIdDTO GetPublisherById(int id)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisherDomain == null) return null;

            return new PublisherNoIdDTO
            {
                Name = publisherDomain.Name
            };
        }

        public PublisherDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO)
        {
            var publisherDomain = new Publishers
            {
                Name = addPublisherRequestDTO.Name
            };

            _dbContext.Publishers.Add(publisherDomain);
            _dbContext.SaveChanges();

            return new PublisherDTO
            {
                Id = publisherDomain.Id,
                Name = publisherDomain.Name
            };
        }

        public PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisherDomain == null) return null;

            publisherDomain.Name = publisherNoIdDTO.Name;
            _dbContext.SaveChanges();

            return publisherNoIdDTO;
        }

        public Publishers? DeletePublisherById(int id)
        {
            // Load Publisher kèm theo Books
            var publisherDomain = _dbContext.Publishers
                                            .Include(p => p.Books)
                                            .FirstOrDefault(p => p.Id == id);

            if (publisherDomain == null) return null;

            // Nếu còn sách tham chiếu thì báo lỗi
            if (publisherDomain.Books != null && publisherDomain.Books.Any())
            {
                throw new Exception($"Cannot delete publisher {id} because there are books referencing it.");
            }

            // Nếu không có sách thì xoá
            _dbContext.Publishers.Remove(publisherDomain);
            _dbContext.SaveChanges();

            return publisherDomain;
        }
    }
}
