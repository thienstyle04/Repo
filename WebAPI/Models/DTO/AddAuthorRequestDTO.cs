using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class AddAuthorRequestDTO
    {
        [MaxLength(int.MaxValue)]
        [Required]
        public int Id { get; set; }
        public string FullName { set; get; }
    }
}
