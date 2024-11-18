// Controllers/StudentsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using problem_assignment_2.Controllers;
using problem_assignment_2.Models;
using problem_assignment_2.ViewModels;

[Route("students")]
public class StudentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("create/{courseId}")]
    public IActionResult Create(int courseId)
    {
        var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);
        if (course == null)
        {
            return NotFound();
        }
        return View(new StudentViewModel { CourseId = courseId });
    }

    [HttpPost("create/{courseId}")]
    public IActionResult Create(int courseId, StudentViewModel model)
    {
        if (ModelState.IsValid)
        {
            var student = new Student
            {
                Name = model.Name,
                Email = model.Email,
                Status = EnrollmentStatus.ConfirmationMessageNotSent,
                CourseId = courseId
            };
            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Details", "Courses", new { id = courseId });
        }
        return View(model);
    }

    [HttpGet("respond/{id}")]
    public IActionResult Respond(int id)
    {
        var student = _context.Students.Include(s => s.Course).FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

[HttpPost("respond/{id}")]
public IActionResult Respond(int id, bool confirm)
{
    var student = _context.Students.Find(id);
    
    if (student == null)
    {
        return NotFound();
    }

    // Update student's status based on confirmation response
    student.Status = confirm ? EnrollmentStatus.EnrollmentConfirmed : EnrollmentStatus.EnrollmentDeclined;
    
    _context.SaveChanges();

    return View("ResponseConfirmation"); // Redirect or show a response confirmation view
}
[HttpGet("confirm")]
public IActionResult Confirm(int id)
{
    // Retrieve the student from the database
    var student = _context.Students.Include(s => s.Course).FirstOrDefault(s => s.Id == id);
    
    if (student == null)
    {
        return View("ConfirmationFailed"); // Return a failure view if student not found
    }

    // Render the Respond view with the student's information
    return View("Respond", student);
}
}