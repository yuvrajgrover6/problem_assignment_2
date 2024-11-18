using problem_assignment_2.Models;
using System.Net;
using System.Net.Mail;
public class EmailService
{
    public void SendEnrollmentConfirmation(Student student, Course course)
    {
        string fromAddress = "workworkhindi@gmail.com";
        string generatedPassword = "fpaz hrbi uknb tozb"; // Use app password if 2FA is enabled
         var smtpClient = new SmtpClient("smtp.gmail.com")
    {
        Port = 587,
        Credentials = new NetworkCredential(fromAddress, generatedPassword),
        EnableSsl = true,
    };

    // Use localhost for development
    string confirmationLink = $"http://localhost:5001/students/confirm?id={student.Id}"; // Update this to your local port

    var mailMessage = new MailMessage()
    {
        From = new MailAddress(fromAddress),
        Subject = "Please Confirm Your Email Address",
        Body = $"<h1>Confirm Your Email</h1><p>Please click <a href=\"{confirmationLink}\">here</a> to confirm your email address.</p>",
        IsBodyHtml = true,
    };

    mailMessage.To.Add(student.Email);

    smtpClient.SendMailAsync(mailMessage).ContinueWith(task =>
    {
        if (task.IsFaulted)
        {
            Console.WriteLine("Error sending email: " + task.Exception.InnerException.Message);
        }
        else
        {
            Console.WriteLine("Confirmation email sent successfully.");
        }
    });
       
}
}