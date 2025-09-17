using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Domain
{
    public class Books
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }

        // 🔹 Navigation Properties
        // Một Publisher có nhiều Book
        public int PublisherID { get; set; }
        public Publishers Publisher { get; set; }

        // Một Book có nhiều Book_Author (N-N với Author)
        public List<Book_Author> Book_Authors { get; set; }

    }
}
