using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IBookRepository _bookRepository;
        public BookController(AppDbContext dbContext, IBookRepository bookRepository)
        {
            _dbContext = dbContext;
            _bookRepository = bookRepository;

        }

        [HttpGet("get-all-books")]
        public IActionResult GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy,
            [FromQuery] bool isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            // su dung reposity pattern  
            var allBooks = _bookRepository.GetAllBooks(filterOn,filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(allBooks);

        }
        [HttpGet]
        [Route("get-book-by-id/{id}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            var bookWithIdDTO = _bookRepository.GetBookById(id);
            return Ok(bookWithIdDTO);
        }

        [HttpPost("add-book")]
        [ValidateModel]
        //[Authorize(Roles = "write")]
        public IActionResult AddBook([FromBody] AddBookRequestDTO addBookRequestDTO)
        {
            if (ValidateAddBook(addBookRequestDTO))
            {
                var bookAdd = _bookRepository.AddBook(addBookRequestDTO);
                return Ok(bookAdd);     
            }
            else return BadRequest(ModelState);
        }

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] AddBookRequestDTO bookDTO)
        {
            var updateBook = _bookRepository.UpdateBookById(id, bookDTO);
            return Ok(updateBook);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            var deleteBook = _bookRepository.DeleteBookById(id);
            return Ok(deleteBook);
        }

        private bool ValidateAddBook(AddBookRequestDTO addBookRequestDTO)
        {
            if (addBookRequestDTO == null)
            {
                ModelState.AddModelError(nameof(addBookRequestDTO), "Please add book data");
                return false;
            }

            // 1. Description không được rỗng
            if (string.IsNullOrEmpty(addBookRequestDTO.Description))
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.Description), "Description cannot be null");
            }

            // 2. Rate trong khoảng 0–5
            if (addBookRequestDTO.Rate < 0 || addBookRequestDTO.Rate > 5)
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.Rate), "Rate must be between 0 and 5");
            }

            // 3. Title không rỗng & không chứa ký tự đặc biệt
            if (string.IsNullOrWhiteSpace(addBookRequestDTO.Title))
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.Title), "Title cannot be empty");
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(addBookRequestDTO.Title, @"^[a-zA-Z0-9\s]+$"))
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.Title), "Title cannot contain special characters (e.g., @, #, $)");
            }

            // 4. Publisher phải tồn tại
            var publisherExists = _dbContext.Publishers.Any(p => p.Id == addBookRequestDTO.PublisherID);
            if (!publisherExists)
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.PublisherID), $"Publisher with Id {addBookRequestDTO.PublisherID} does not exist");
            }

            // 5. Kiểm tra AuthorIds
            if (addBookRequestDTO.AuthorIds != null && addBookRequestDTO.AuthorIds.Any())
            {
                // 5.1 Kiểm tra trùng lặp trong danh sách client gửi lên
                var duplicateAuthorIds = addBookRequestDTO.AuthorIds
                    .GroupBy(id => id)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                if (duplicateAuthorIds.Any())
                {
                    ModelState.AddModelError(nameof(addBookRequestDTO.AuthorIds),
                        $"Duplicate AuthorIds found in request: {string.Join(", ", duplicateAuthorIds)}");
                }

                // 5.2 Kiểm tra mỗi Author có tồn tại trong DB không
                foreach (var authorId in addBookRequestDTO.AuthorIds)
                {
                    var authorExists = _dbContext.Authors.Any(a => a.Id == authorId);
                    if (!authorExists)
                    {
                        ModelState.AddModelError(nameof(addBookRequestDTO.AuthorIds), $"Author with Id {authorId} does not exist");
                    }
                }

            }
            return ModelState.ErrorCount == 0;
        }
    }

}

