using System.ComponentModel.DataAnnotations;


namespace problem_assignment_2.ViewModels;
public class CourseViewModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Instructor { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required]
    [RegularExpression(@"^[1-9][A-Z]\d{2}$", ErrorMessage = "Room number must be in the format: a single digit, a single capital letter, and 2 digits, e.g. 3G15, 1C07")]
    public required string RoomNumber { get; set; }
}