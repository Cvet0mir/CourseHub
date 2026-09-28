using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseHub.ViewModels.Course
{
	public class CourseDataViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Subject { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public string Difficulty { get; set; }

		public string TeacherFullName { get; set; }
	}
}
