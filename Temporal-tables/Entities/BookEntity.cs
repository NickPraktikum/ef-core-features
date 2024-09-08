namespace ShadowProps.Models
{
    using System.ComponentModel.DataAnnotations;

    internal class BookEntity
    {
        [Key]
        public long BookId { get; set; }

        [Required]
        [StringLength(100)]
        public string Isbn = default!;

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = default!;

        [Required]
        public int Pages { get; set; }

    }
}
