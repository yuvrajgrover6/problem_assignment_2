// Models/Student.cs
using System.ComponentModel.DataAnnotations;
namespace problem_assignment_2.Models;
public class Student
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public EnrollmentStatus Status { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }
}