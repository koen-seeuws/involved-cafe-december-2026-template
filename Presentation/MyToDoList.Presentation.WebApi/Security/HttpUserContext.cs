using MyToDoList.Application.Abstractions;

namespace MyToDoList.Presentation.WebApi.Security;

/// <summary>
/// Resolves the current user from the HTTP request.
/// Uses the authenticated identity when there is one; otherwise falls back to the
/// <c>X-User</c> header (a stand-in until real authentication is added).
/// </summary>
public sealed class HttpUserContext : IUserContext
{
    public const string UserHeader = "X-User";

    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserName
    {
        get
        {
            var httpContext = _httpContextAccessor.HttpContext
                ?? throw new InvalidOperationException("There is no active HTTP request.");

            if (httpContext.User.Identity is { IsAuthenticated: true, Name: { Length: > 0 } name })
                return name;

            var header = httpContext.Request.Headers[UserHeader].ToString().Trim();

            return header.Length > 0
                ? header
                : throw new UnauthorizedAccessException($"Identify yourself with the '{UserHeader}' header.");
        }
    }
}
