using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Domain
{
    public class Publishers
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        // mo ta : mot tac gia cao the co nhieu sach
        public List<Books> Books { get; set; }

    }
}
