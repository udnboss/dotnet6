using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class MyUserStore : IUserStore<IdentityUser>, IUserPasswordStore<IdentityUser>, IUserClaimStore<IdentityUser>
{
    private MyContext _context;

    public MyUserStore(MyContext context)
    {
        _context = context;
    }

    private Login CreateLoginFromIdentityUser(IdentityUser user)
    {
        var login = new Login {Id = user.Id, UserName = user.UserName, NormalizedUserName = user.NormalizedUserName, PasswordHash = user.PasswordHash};
        return login;
    }
    public Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        
        try 
        {
            var login = CreateLoginFromIdentityUser(user);
            _context.Logins.Add(login);
            
            //create a user for the login
            var u = new User { Id = Guid.NewGuid().ToString(), Email = login.UserName, LoginId = login.Id, Name = login.UserName };
            _context.Users.Add(u);

            var adminRoleCode = AppRole.Admin.GetCode();
            var adminRole = _context.Roles.First(x => x.Code == adminRoleCode);

            //grant admin role
            var userRole = new UserRole { Id = Guid.NewGuid().ToString(), RoleId = adminRole.Id, UserId = u.Id };
            _context.UserRoles.Add(userRole);

            _context.SaveChanges();

            return Task.FromResult(IdentityResult.Success);
        }
        catch (Exception ex)
        {
            return Task.FromResult(IdentityResult.Failed(new IdentityError
            {
                Code = "DuplicateUserId",
                Description = "A user with this id already exists. " + ex.Message
            }));
        }
    }

    public Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        try
        {
            var login = _context.Logins.First(x => x.UserName == user.UserName);
            _context.Logins.Remove(login);
            _context.SaveChanges();
            return Task.FromResult(IdentityResult.Success);
        }
        catch
        {
            return Task.FromResult(IdentityResult.Failed(new IdentityError
            {
                Code = "UserNotFound",
                Description = "The user could not be found."
            }));
        }
    }

    public void Dispose()
    {
        // Nothing to dispose.
    }

    public Task<IdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        var login = _context.Logins.First(x => x.Id == userId);
        var user = new IdentityUser { Id = login.Id, UserName = login.UserName, PasswordHash = login.PasswordHash, NormalizedUserName = login.NormalizedUserName };
        return Task.FromResult(user);
    }

    public Task<IdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        var login  = _context.Logins.First(u => u.NormalizedUserName == normalizedUserName);
        var user = new IdentityUser { Id = login.Id, UserName = login.UserName, PasswordHash = login.PasswordHash, NormalizedUserName = login.NormalizedUserName };
        return Task.FromResult(user);
    }

    public Task<string> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.NormalizedUserName);
    }

    public Task<string> GetPasswordHashAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PasswordHash);
    }

    public Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Id.ToString());
    }

    public Task<string> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName);
    }

    public Task<bool> HasPasswordAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PasswordHash != null);
    }

    public Task SetNormalizedUserNameAsync(IdentityUser user, string normalizedName, CancellationToken cancellationToken)
    {
        user.NormalizedUserName = normalizedName;
        return Task.CompletedTask;
    }

    public Task SetPasswordHashAsync(IdentityUser user, string passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;
        return Task.CompletedTask;
    }

    public Task SetUserNameAsync(IdentityUser user, string userName, CancellationToken cancellationToken)
    {
        user.UserName = userName;
        return Task.CompletedTask;
    }

    public Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        try
        {
            var login = CreateLoginFromIdentityUser(user);
            _context.Logins.Entry(login).State = EntityState.Modified;
            _context.SaveChanges();
            return Task.FromResult(IdentityResult.Success);
        }
        catch
        {
            return Task.FromResult(IdentityResult.Failed(new IdentityError
            {
                Code = "ConcurrencyFailure",
                Description = "The user has been updated by another process."
            }));
        }
    }

    public Task<IList<Claim>> GetClaimsAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        var login = _context.Logins.First(x => x.UserName == user.UserName);
        var dbUser = _context.Users.First(u => u.LoginId == login.Id);
            
        //load user permissions
        var claims = _context.Users
            .Where(u => u.Id == dbUser.Id) //.Include("UserRoles.Role.RolePermissions.Permission")
            .SelectMany(u => u.UserRoles!)
            .Select(ur => ur.Role!)
            .SelectMany(r => r.RolePermissions!)
            .Select(rp => rp.Permission!)
            .Distinct()                    
            .Select(p => new Claim("Permission", p.Code))
            .ToList() as IList<Claim>;

        return Task.FromResult(claims);
    }

    public Task AddClaimsAsync(IdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task ReplaceClaimAsync(IdentityUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveClaimsAsync(IdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<IdentityUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
