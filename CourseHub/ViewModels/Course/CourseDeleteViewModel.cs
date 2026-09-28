using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseHub.ViewModels.Course
{
    public class CourseDeleteViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
