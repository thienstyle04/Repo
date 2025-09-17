using System.ComponentModel.DataAnnotations;
namespace WebAPI.Models.Domain
{
    public class Book_Author
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        // mo ta : mot quyen sach co nhieu tac gia
        public Books Books { get; set; }
        public int AuthorId { get; set; }
        public Authors Author { get; set; }
    }
}
