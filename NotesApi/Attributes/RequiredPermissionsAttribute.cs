using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class RequiredPermissionsAttribute : Attribute, IAuthorizationFilter
{
    public AppPermission[] RequiredPermissions { get; set; }

    public RequiredPermissionsAttribute(params AppPermission[] permissions)
    {
        RequiredPermissions = permissions;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Perform your authorization logic here. For example:
        if (context.HttpContext.User.Identity is null || !context.HttpContext.User.Identity.IsAuthenticated ||
            !RequiredPermissions.All(permission => context.HttpContext.User.HasClaim("Permission", permission.GetCode())))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}