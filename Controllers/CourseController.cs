// Controllers/CoursesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using problem_assignment_2.Models;

namespace problem_assignment_2.Controllers;
[Route("courses")]
public class CoursesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly EmailService _emailService;

    public CoursesController(ApplicationDbContext context, EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var courses = _context.Courses.Include(c => c.Students).ToList();
        return View(courses);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    public IActionResult Create(Course course)
    {
        if (ModelState.IsValid)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    [HttpGet("{id}")]
    public IActionResult Details(int id)
    {
        var course = _context.Courses.Include(c => c.Students).FirstOrDefault(c => c.Id == id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }
[HttpPost("/courses/send-confirmations")]
public IActionResult SendConfirmations(int id)
{
    var course = _context.Courses.Include(c => c.Students).FirstOrDefault(c => c.Id == id);
    if (course == null)
    {
        return NotFound();
    }

    // Get students who have not yet received a confirmation message
    var studentWithStatus = course.Students.Where(s => s.Status == EnrollmentStatus.ConfirmationMessageNotSent).ToList();
    
    // Track the number of emails sent
    int emailsSentCount = 0;

    foreach (var student in studentWithStatus)
    {
        // Send the confirmation email
        _emailService.SendEnrollmentConfirmation(student, course);
        
        // Update the student's status
        student.Status = EnrollmentStatus.ConfirmationMessageSent;
        
        // Increment the count of emails sent
        emailsSentCount++;
    }

    // Save changes to the database
    _context.SaveChanges();

    // Only set TempData message if at least one email was sent
    if (emailsSentCount > 0)
    {
        TempData["EmailStatus"] = $"{emailsSentCount} confirmation emails have been sent successfully."; // Set a message in TempData
    }
    else{
        TempData["EmailStatus"] = "No confirmation emails were sent"; // Set a message in TempData
    }

    TempData["numEmailsSent"] = emailsSentCount; // Set the number of emails sent in TempData

    return RedirectToAction(nameof(Details), new { id });
}
    [HttpGet("edit/{id}")]
    public IActionResult Edit(int id)
    {
        var course = _context.Courses.Find(id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    [HttpPost("edit/{id}")]
    public IActionResult Edit(int id, Course course)
    {
        if (id != course.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(course);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }
}




