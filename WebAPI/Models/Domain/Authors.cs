using System.ComponentModel.DataAnnotations;
namespace WebAPI.Models.Domain
{
    public class Authors
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        // mo ta: mot tac pham co the co nhieu tac gia
        public List<Book_Author> Book_Authors { get; set; }
}
}
