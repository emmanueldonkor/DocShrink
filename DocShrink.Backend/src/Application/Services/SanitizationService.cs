using Core.Interfaces;
using Ganss.Xss;

namespace Application.Services;

public class SanitizationService : ISanitizationService
{
    private readonly HtmlSanitizer sanitizer;

    public SanitizationService()
    {
        sanitizer = new HtmlSanitizer();
        sanitizer.AllowedSchemes.Add("data");
    }

    public string SanitizeInput(string input)
    {
        return sanitizer.Sanitize(input);
    }
}