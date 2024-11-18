// Middleware/FirstVisitMiddleware.cs
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public class FirstVisitMiddleware
{
    private readonly RequestDelegate _next;

    public FirstVisitMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        const string cookieName = "FirstVisit";
        
        if (!context.Request.Cookies.ContainsKey(cookieName))
        {
            // Set the cookie if it doesn't exist
            var firstVisitDate = DateTime.UtcNow.ToString("u"); // Store in UTC format
            context.Response.Cookies.Append(cookieName, firstVisitDate, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1) // Set expiration as needed
            });

            // Add a message for first-time visitors
            context.Items["WelcomeMessage"] = "Welcome to your first visit!";
        }
        else
        {
            // Retrieve the date from the cookie
            var firstVisitDate = context.Request.Cookies[cookieName];
            context.Items["WelcomeMessage"] = $"Welcome back! You first visited on {firstVisitDate}.";
        }

        await _next(context);
    }
}