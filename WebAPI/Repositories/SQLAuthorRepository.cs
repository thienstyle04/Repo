using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models.Domain;
using WebAPI.Models.DTO;
using WebAPI.Models.DTO;

namespace WebAPI.Repositories
{
    public class SQLAuthorRepository: IAuthorRepository
    {
        private readonly AppDbContext _dbContext;
        public SQLAuthorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Lấy danh sách tất cả tác giả
        public List<AuthorDTO> GellAllAuthors()
        {
            var allAuthors = _dbContext.Authors.Select(author => new AuthorDTO()
            {
                Id = author.Id,
                FullName = author.FullName
            }).ToList();

            return allAuthors;
        }

        // Lấy tác giả theo ID (trả về DTO không có Id)
        public AuthorNoIdDTO GetAuthorById(int id)
        {
            var authorDomain = _dbContext.Authors.FirstOrDefault(n => n.Id == id);

            if (authorDomain == null) return null;

            return new AuthorNoIdDTO()
            {
                FullName = authorDomain.FullName
            };
        }

        // Thêm tác giả mới
        public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var authorDomain = new Authors
            {
                FullName = addAuthorRequestDTO.FullName
            };

            _dbContext.Authors.Add(authorDomain);
            _dbContext.SaveChanges();

            return addAuthorRequestDTO;
        }

        // Cập nhật tác giả theo Id
        public AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO)
        {
            var authorDomain = _dbContext.Authors.FirstOrDefault(n => n.Id == id);

            if (authorDomain != null)
            {
                authorDomain.FullName = authorNoIdDTO.FullName; // ✅ sửa lỗi gán sai
                _dbContext.SaveChanges();
                return authorNoIdDTO; // ✅ không còn dùng biến chưa khai báo
            }

            return null;
        }

        // Xóa tác giả theo Id
        public Authors? DeleteAuthorById(int id)
        {
            var authorDomain = _dbContext.Authors.FirstOrDefault(n => n.Id == id);

            if (authorDomain != null)
            {
                _dbContext.Authors.Remove(authorDomain);
                _dbContext.SaveChanges();
            }

            return authorDomain;
        }
    }
}
