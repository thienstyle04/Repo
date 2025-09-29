using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Repositories;
using WebAPI.Models.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(AppDbContext dbContext, IAuthorRepository authorRepository)
        {
            _dbContext = dbContext;
            _authorRepository = authorRepository;
        }

        // GET: api/authors/get-all-authors
        [HttpGet("get-all-authors")]
        public IActionResult GetAllAuthors()
        {
            var allAuthors = _authorRepository.GellAllAuthors();
            return Ok(allAuthors);
        }

        // GET: api/authors/get-authors-by-id/1
        [HttpGet("get-authors-by-id/{id}")]
        public IActionResult GetAuthorsById([FromRoute] int id)
        {
            var authorWithIdDTO = _authorRepository.GetAuthorById(id);
            if (authorWithIdDTO == null)
                return NotFound($"Author with Id = {id} not found.");

            return Ok(authorWithIdDTO);
        }

        // POST: api/authors/add-authors
        [HttpPost("add-authors")]
        [ValidateModel]
        public IActionResult AddAuthors([FromBody] AddAuthorRequestDTO addAuthorRequestDTO)
        {
            //var authorAdd = _authorRepository.AddAuthor(addAuthorRequestDTO);
            //return Ok(authorAdd);
            if (ValidateAddAuthor(addAuthorRequestDTO))
            {
                var addAuthor = _authorRepository.AddAuthor(addAuthorRequestDTO);
                return Ok(addAuthor);
            }
            else return BadRequest(ModelState);

        }

        // PUT: api/authors/update-authors-by-id/1
        [HttpPut("update-authors-by-id/{id}")]
        public IActionResult UpdateAuthorsById([FromRoute] int id, [FromBody] AuthorNoIdDTO authorNoIdDTO)
        {
            var updateAuthor = _authorRepository.UpdateAuthorById(id, authorNoIdDTO);
            if (updateAuthor == null)
                return NotFound($"Author with Id = {id} not found.");

            return Ok(updateAuthor);
        }

        // DELETE: api/authors/delete-authors-by-id/1
        [HttpDelete("delete-authors-by-id/{id}")]
        public IActionResult DeleteAuthorById([FromRoute] int id)
        {
            var deleteAuthor = _authorRepository.DeleteAuthorById(id);
            if (deleteAuthor == null)
                return NotFound($"Author with Id = {id} not found.");

            return Ok(deleteAuthor);
        }

        private bool ValidateAddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
        {
            if (string.IsNullOrWhiteSpace(addAuthorRequestDTO.FullName))
            {
                ModelState.AddModelError(nameof(addAuthorRequestDTO.FullName), "title khong duoc de trong!!!!!");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
    }
}
