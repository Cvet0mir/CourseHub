using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string MainSubject { get; set; }

        [Required]
        [Range(0, 100)]
        [Column("Years of Experience")]
        public int YearsExperience { get; set; }

        [StringLength(150, MinimumLength = 2)]
        public string Address { get; set; }

        [Range(18, 100)]
        public int Age { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
