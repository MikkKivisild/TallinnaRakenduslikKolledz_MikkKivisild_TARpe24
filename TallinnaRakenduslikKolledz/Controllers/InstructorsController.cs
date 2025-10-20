using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikKolledz.Data;
using TallinnaRakenduslikKolledz.Models;

namespace TallinnaRakenduslikKolledz.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly SchoolContext _context;
        public InstructorsController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id, int? coureId)
        {
            var vm = new InstructorIndexData();
            vm.Instructors = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                .ToListAsync();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var instructor = new Instructor();
            instructor.CourseAssignments = new List<CourseAssignment>();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor, string selectedCourses)
        {
            if (selectedCourses != null)
            {
                instructor.CourseAssignments = new List<CourseAssignment>();
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = new CourseAssignment
                    {
                        InstructorID = instructor.ID,
                        CourseID = course
                    };
                    instructor.CourseAssignments.Add(courseToAdd);
                }
            }
            ModelState.Remove("selectedCourses");
            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(instructor);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            { return NotFound(); }
            var deletableInstructor = await _context.Instructors
                .FirstOrDefaultAsync(s => s.ID == id);
            if (deletableInstructor == null) { return NotFound(); }
            return View(deletableInstructor);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Instructor deletableInstructor = await _context.Instructors
                .SingleAsync(i => i.ID == id);
            _context.Instructors.Remove(deletableInstructor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var allCourse = _context.Courses;

            var instructorCourses = new HashSet<int>(instructor.CourseAssignments.Select(c => c.CourseID));
            var vm = new List<AssignedCourseData>();
            foreach (var course in allCourse)
            {
                vm.Add(new AssignedCourseData
                {
                    CourseID = course.CourseId,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseId)
                });
            }
            ViewData["Courses"] = vm;
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return NotFound(); }
            var instructor = await _context.Instructors.FirstOrDefaultAsync(x => x.ID == id);
            if (instructor == null) { return NotFound(); }
            return View(instructor);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = _context.Instructors.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Instructor instructor)
        {
            if (id != instructor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(instructor);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }
    }
}
