namespace TemporalTable.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TemporalTable.Interfaces;

    internal class BookEntity : ISoftDelete, IVersion
    {
        [Key]
        [Column(nameof(Id), Order = 1)]
        public long Id { get; set; }

        [Required]
        [Column(nameof(AuthorId), Order = 2)]
        public long AuthorId { get; set; }

        [Required]
        [StringLength(100)]
        [Column(nameof(Title), Order = 10)]
        public string Isbn = default!;

        [Required]
        [StringLength(100)]
        [Column(nameof(Title), Order = 11)]
        public string Title { get; set; } = default!;

        [Required]
        [Column(nameof(Pages), Order = 12)]
        public int Pages { get; set; }

        [Required]
        [Column(nameof(Price), Order = 13)]
        public float Price { get; set; }

        [Required]
        [Column(nameof(IsDeleted), Order = 14)]
        public bool IsDeleted { get; set; } = false;

        [Column(nameof(Version), Order = 15)]
        [Required]
        public int Version { get; set; } = 1;

        [Column(nameof(DeletedAt), Order = 30)]
        public DateTimeOffset? DeletedAt { get; set; } = null!;
    }
}
