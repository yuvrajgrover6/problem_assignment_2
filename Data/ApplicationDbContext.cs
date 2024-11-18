using Microsoft.EntityFrameworkCore;
using problem_assignment_2.Models;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public required DbSet<Course> Courses { get; set; }
    public required DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>()
            .Property(s => s.Status)
            .HasConversion<string>();
    }
}