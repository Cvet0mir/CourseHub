using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseHub.ViewModels.Course
{
    public class CourseEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name is required!")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "The Name has to be between 2 and 150 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Name is required!")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "The Name has to be between 2 and 150 characters!")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "The Starting Date is required!")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The Ending Date is required!")]
        public DateTime EndDate { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "The Difficulty must be between 2 and 50!")]
        public string Difficulty { get; set; }
    }
}
