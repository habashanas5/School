using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace School.Models
{
    public class Subject
    {
        [Key]
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter numbers only.")]
        [DisplayName("Subject Identity")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Subject Name")]
        public string Name { get; set; }
    }
}
