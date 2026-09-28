using System.ComponentModel.DataAnnotations;

namespace CourseHub.ViewModels.Teacher
{
    public class TeacherEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The First Name is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The First Name has to be between 2 and 100 characters!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Last Name is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The Last Name has to be between 2 and 100 characters!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Main Subject is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The Main Subject has to be between 2 and 100 characters!")]
        public string MainSubject { get; set; }

        [Required(ErrorMessage = "The Years of Experience are required!")]
        [Range(0, 100, ErrorMessage = "The Years of Experience must be between 0 and 100!")]
        public int YearsExperience { get; set; }

        [StringLength(150, MinimumLength = 2, ErrorMessage = "The Address must be between 2 and 150!")]
        public string Address { get; set; }

        [Range(18, 100, ErrorMessage = "The Age must be between 18 and 100!")]
        public int Age { get; set; }
    }
}
