namespace Experiments.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// A simple book model.
    /// </summary>
    public class Book
    {
        [Column(nameof(Id), Order = 1)]
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Isbn = default!;

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = default!;

        [Required]
        public int Pages { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public long AuthorId { get; set; }
    }
}
