using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace School.Models
{
    public class Teacher
    {
        [Key]
        [Required]
        [DisplayName("Student Identity ")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter numbers only.")]
        public int Id { get; set; }
        [Required]
        [DisplayName("Student Name ")]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [DisplayName("Student Email ")]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter numbers only.")]
        [DisplayName("Student Phone number ")]
        public int PhoneNumber { get; set; }


    }
}
