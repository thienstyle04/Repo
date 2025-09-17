using System.ComponentModel.DataAnnotations;

public class AddBookRequestDTO
{
    [Required]
    [MaxLength(200)]
    public string? Title { get; set; }

    public string? Description { get; set; }
    public bool IsRead { get; set; }
    public DateTime? DateRead { get; set; }

    [Range(0, 10)]
    public int? Rate { get; set; }

    public string? Genre { get; set; }
    public string? CoverUrl { get; set; }

    public DateTime DateAdded { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    [Required]
    public int PublisherID { get; set; }

    public List<int> AuthorIds { get; set; } = new List<int>();
}
