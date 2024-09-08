namespace ShadowProps.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// A simple author model.
    /// </summary>
    internal class AuthorEntity
    {
        #region properties

        [Key]
        public long AuthorId { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; } = default!;

        [Required]
        [StringLength(20)]
        public string SecondName { get; set; } = default!;

        [Required]
        public int Age { get; set; }

        [Required]
        public DateTimeOffset BirthDate { get; set; }

        public List<BookEntity> Books { get; set; } = new();

        #endregion
    }
}
