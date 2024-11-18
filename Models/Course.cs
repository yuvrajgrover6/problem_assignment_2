// Models/Course.cs
using System.ComponentModel.DataAnnotations;
namespace problem_assignment_2.Models;
public class Course
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Instructor { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    [RegularExpression(@"^[1-9][A-Z]\d{2}$", ErrorMessage = "Room number must be in the format: a single digit, a single capital letter, and 2 digits, e.g. 3G15, 1C07")]
    public string RoomNumber { get; set; }

    public List<Student> Students { get; set; } = new List<Student>();
}



