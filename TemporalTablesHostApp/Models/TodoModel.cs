namespace TemporalTablesHostApp.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// Represents a simple to do task.
    /// </summary>
    public class TodoModel
    {
        /// <summary>
        /// The unique identifier of the ToDo.
        /// </summary>
        [Key]
        [Column(nameof(Id), Order = 1)]
        public long Id { get; set; }

        /// <summary>
        /// The text inside of the ToDo that represents the task of the ToDo.
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column(nameof(Todo), TypeName = "nvarchar(max)", Order = 10)]
        public string Todo { get; set; } = default!; 
    }
}
