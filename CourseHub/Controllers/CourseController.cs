using System.Diagnostics;
using CourseHub.Models;
using Microsoft.AspNetCore.Mvc;
using Database;
using Database.Entities;
using CourseHub.ViewModels.Course;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace CourseHub.Controllers
{
    public class CourseController : Controller
    {
        private readonly CoursesDbContext _context;

        public CourseController(CoursesDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                .Include(c => c.Teacher)
                .Select(c => new CourseIndexViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subject = c.Subject,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Difficulty = c.Difficulty,
                    TeacherFullName = c.Teacher.FirstName + " " + c.Teacher.LastName
                })
                .ToListAsync();
            return View(courses);
        }

        public async Task<IActionResult> Create()
        {
            var teachers = await _context.Teachers.ToListAsync();
            ViewBag.Teachers = teachers;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var course = new Course
            {
                Name = model.Name,
                Subject = model.Subject,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Difficulty = model.Difficulty,
                TeacherId = model.TeacherId
            };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (course == null) return NotFound();

            var model = new CourseDataViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Subject = course.Subject,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                Difficulty = course.Difficulty,
                TeacherFullName = course.Teacher.FirstName + " " + course.Teacher.LastName
            };
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            var model = new CourseEditViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Subject = course.Subject,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                Difficulty = course.Difficulty,
                TeacherId = course.TeacherId
            };

            var teachers = await _context.Teachers.ToListAsync();
            ViewBag.Teachers = teachers;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CourseEditViewModel model)
        {
            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            course.Name = model.Name;
            course.Subject = model.Subject;
            course.StartDate = model.StartDate;
            course.EndDate = model.EndDate;
            course.Difficulty = model.Difficulty;
            course.TeacherId = model.TeacherId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses
                .Select(c => new CourseDeleteViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .FirstOrDefaultAsync(c => c.Id == id);
            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, string fullName)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}