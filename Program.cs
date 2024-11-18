using Microsoft.EntityFrameworkCore;
using problem_assignment_2.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<CookieService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
app.UseMiddleware<FirstVisitMiddleware>();
app.Urls.Add("http://localhost:5001/"); // Set your desired port here

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();