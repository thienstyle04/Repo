using System.ComponentModel.DataAnnotations;
namespace WebAPI.Models.Domain
{
    public class Book_Author
    {
        [Key]
        public int Id { get; set; }

        // 🔹 Foreign Key đến Book
        public int BookId { get; set; }
        public Books Book { get; set; }

        // 🔹 Foreign Key đến Author
        public int AuthorId { get; set; }
        public Authors Author { get; set; }
    }
}
