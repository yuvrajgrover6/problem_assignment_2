using System.ComponentModel.DataAnnotations;


namespace problem_assignment_2.ViewModels;
public class StudentViewModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public int CourseId { get; set; }
}