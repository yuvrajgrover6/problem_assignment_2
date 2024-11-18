// Services/CookieService.cs
public class CookieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DateTime? GetFirstVisitDate()
    {
        var cookie = _httpContextAccessor.HttpContext.Request.Cookies["FirstVisit"];
        if (cookie != null)
        {
            return DateTime.Parse(cookie);
        }
        return null;
    }

    public void SetFirstVisitDate()
    {
        var options = new CookieOptions { Expires = DateTime.Now.AddYears(1) };
        _httpContextAccessor.HttpContext.Response.Cookies.Append("FirstVisit", DateTime.Now.ToString(), options);
    }
}