using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class AddRole
    {
        [Required, StringLength(256)]
        public string Name { get; set; }
    }
}
