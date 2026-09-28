using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseHub.ViewModels.Teacher
{
    public class TeacherDataViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string MainSubject { get; set; }

        public int YearsExperience { get; set; }

        public string Address { get; set; }

        public int Age { get; set; }
    }
}
