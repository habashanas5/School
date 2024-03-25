using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class AssignedMaterial
    {
        [Key]
        [Required(ErrorMessage = "Please enter a valid AssignedMaterial ID.")]
        public int AssignedMaterialID { get; set; }

        [Required(ErrorMessage = "Please select a Student.")]
        public int StudentID { get; set; }

        [Required(ErrorMessage = "Please select a Subject.")]
        public int SubjectID { get; set; }

        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
