using System.ComponentModel.DataAnnotations;

namespace Assignment1_Server.Models
{
    public class Person
    {
        public static readonly string[] GENDERS = { "M", "F", "Other" };

        [Required]
        public int Id { get; set; }
        [Required, StringLength(100, ErrorMessage = "First name is too long")]
        public string FirstName { get; set; }
        [Required, StringLength(100, ErrorMessage = "Last name is too long")]
        public string LastName { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        [Required, Range(0, 200, ErrorMessage = "Invalid age")]
        public int Age { get; set; }
        [Required, Range(0, 1000, ErrorMessage = "Invalid weight")]
        public float Weight { get; set; }
        [Required, Range(0, 1000, ErrorMessage = "Invalid height")]
        public int Height { get; set; }
        [Required]
        public string Sex { get; set; }
    }


}