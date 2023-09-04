public class CustomAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public CustomAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path;

        // Check if the request is for the protected route
        if (path.StartsWithSegments("/protected"))
        {
            // Perform your authorization logic here. For example:
            if (context.User.Identity is not null && context.User.Identity.IsAuthenticated)
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }
        else
        {
            await _next(context);
        }
    }
}
