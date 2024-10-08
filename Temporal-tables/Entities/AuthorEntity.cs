﻿namespace TemporalTable.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TemporalTable.Interfaces;

    /// <summary>
    /// A simple author model.
    /// </summary>
    public class AuthorEntity : ISoftDelete, IVersion
    {
        #region properties

        [Key]
        [Column(nameof(Id), Order = 1)]
        public long Id { get; set; }

        [Required]
        [StringLength(20)]
        [Column(nameof(FirstName), Order = 10)]
        public string FirstName { get; set; } = default!;

        [Required]
        [StringLength(20)]
        [Column(nameof(SecondName), Order = 11)]
        public string SecondName { get; set; } = default!;

        [Required]
        [Column(nameof(BirthDate), Order = 12)]
        public DateTimeOffset BirthDate { get; set; }

        [Required]
        [Column(nameof(IsDeleted), Order = 13)]
        public bool IsDeleted { get; set; } = false;

        [Required]
        [Column(nameof(Version), Order = 14)]
        public int Version { get; set; } = 1;

        [Column(nameof(DeletedAt), Order = 30)]
        public DateTimeOffset? DeletedAt { get; set; } = null!;

        [ForeignKey("AuthorId")]
        public ICollection<BookEntity>? Books { get; set; } = new List<BookEntity>();

        #endregion
    }
}
