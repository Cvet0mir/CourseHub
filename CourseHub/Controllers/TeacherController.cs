using System.Diagnostics;
using CourseHub.Models;
using Microsoft.AspNetCore.Mvc;
using Database;
using CourseHub.ViewModels.Teacher;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database.Entities;


namespace CourseHub.Controllers
{
    public class TeacherController : Controller
    {
        private readonly CoursesDbContext _context;

        public TeacherController(CoursesDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeacherCreateViewModel model)
        {
            Console.WriteLine("Hello");
            if (!ModelState.IsValid) return View(model);

            var teacher = new Teacher
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MainSubject = model.MainSubject,
                YearsExperience = model.YearsExperience,
                Address = model.Address,
                Age = model.Age
            };
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var teachers = await _context.Teachers
                .Select(t => new TeacherIndexViewModel
                {
                    Id = t.Id,
                    FullName = t.FirstName + " " + t.LastName,
                    MainSubject = t.MainSubject,
                    YearsExperience = t.YearsExperience,
                    Age = t.Age
                })
                .ToListAsync();

            return View(teachers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var teacher = await _context.Teachers
                .Where(t => t.Id == id)
                .Select(t => new TeacherDataViewModel
                {
                    Id = t.Id,
                    FullName = t.FirstName + " " + t.LastName,
                    MainSubject = t.MainSubject,
                    YearsExperience = t.YearsExperience,
                    Address = t.Address,
                    Age = t.Age
                })
                .FirstOrDefaultAsync();
            if (teacher == null) return NotFound();

            return View(teacher);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _context.Teachers
                .Where(t => t.Id == id)
                .Select(t => new TeacherDeleteViewModel
                {
                    Id = t.Id,
                    FullName = t.FirstName + " " + t.LastName
                })
                .FirstOrDefaultAsync();
            if (teacher == null) return NotFound();

            return View(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TeacherDeleteViewModel teacherDeleteModel)
        {
            Console.WriteLine("Hello 1");
            var teacher = await _context.Teachers.FindAsync(teacherDeleteModel.Id);
            if (teacher == null) return NotFound();
            Console.WriteLine("Hello 2");

            //var teacherDeleteModel = new TeacherDeleteViewModel
            //{
            //    Id = teacher.Id,
            //    FullName = teacher.FirstName + " " + teacher.LastName
            //};
            //if (teacher.Courses.Count == 0)
            //{
            //    Console.WriteLine("Hello 3");
            //    _context.Teachers.Remove(teacher);
            //    await _context.SaveChangesAsync();
            //    Console.WriteLine("Hello 4");
            //    return RedirectToAction(nameof(Index));
            //}

            bool hasAssignedCourses = await _context.Courses.AnyAsync(c => c.TeacherId == teacher.Id);
            ViewBag.ErrorMsg = "";

            Console.WriteLine("Hello 5");
            if (hasAssignedCourses)
            {
                ViewBag.ErrorMsg = "Cannot delete teacher with assigned courses.";
                return View(teacherDeleteModel);
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return NotFound();

            var model = new TeacherEditViewModel
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                MainSubject = teacher.MainSubject,
                YearsExperience = teacher.YearsExperience,
                Address = teacher.Address,
                Age = teacher.Age
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TeacherEditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var teacher = await _context.Teachers.FindAsync(model.Id);
            if (teacher == null) return NotFound();

            teacher.FirstName = model.FirstName;
            teacher.LastName = model.LastName;
            teacher.MainSubject = model.MainSubject;
            teacher.YearsExperience = model.YearsExperience;
            teacher.Address = model.Address;
            teacher.Age = model.Age;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
