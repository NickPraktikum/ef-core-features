namespace Experiments.Models
{
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    /// A simple author model.
    /// </summary>
    public class Author
    {
        #region properties

        [Key]
        public long Id { get; set; }

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

        public List<Book> Books { get; set; } = new();

        #endregion
    }
}
