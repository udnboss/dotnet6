using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MyContext _context;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(MyContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        private List<Claim> LoadLoginClaims(string loginId)
        {
            var user = _context.Users.First(u => u.LoginId == loginId);
            
            //load user permissions
            var claims = _context.Users
                .Where(u => u.Id == user.Id)
                .SelectMany(u => u.UserRoles!)
                .Select(ur => ur.Role!)
                .SelectMany(r => r.RolePermissions!)
                .Select(rp => rp.Permission!)
                .Distinct()                    
                .Select(p => new Claim("Permission", p.Code))
                .ToList();

            return claims;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            // get the user by username
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // verify his password
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                return Unauthorized("Invalid password");
            }

            // do something with the authenticated user
            return Ok();

            // var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // var login = _context.Logins.FirstOrDefault(x => x.UserName == model.UserName && x.PasswordHash == passwordHash);

            // if (login is null)
            // {
            //     return BadRequest("Invalid username or password");                
            // }

            // var claims = LoadLoginClaims(login);

            // // create a claims identity with the claims and the authentication scheme
            // var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // // create a claims principal with the identity
            // var principal = new ClaimsPrincipal(identity);

            // var user = new IdentityUser { Id = login.Id, UserName = login.UserName };

            // create a cookie authentication properties with additional options
            // var properties = new CookieAuthenticationProperties
            // {
            //     IsPersistent = true,
            //     // add other properties as needed
            // };

            // sign in the user and set the cookie
            // await _signInManager.SignInAsync(user, true);

            // HttpContext.User = new ClaimsPrincipal(identity);

            // set a cookie named "mycookie" with the value "hello" and an expiration date of one day
            // Response.Cookies.Append("mycookie", "hello", new CookieOptions
            // {
            //     Expires = DateTime.Now.AddDays(1)
            // });

            
            // return Ok();
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            
            
            try
            {
                // var login = new Login { Id = Guid.NewGuid().ToString(), UserName = model.UserName };
                // _context.Logins.Add(login);
                // _context.SaveChanges();

                // create a new user with username and email
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.UserName,
                    Id = Guid.NewGuid().ToString(),
                    PhoneNumber = "123-456-7890",
                    TwoFactorEnabled = false
                };

                // create the user with password
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                var claims = LoadLoginClaims(user.Id);
                // add the claims to the user
                await _userManager.AddClaimsAsync(user, claims);

                // sign in the user and set the cookie
                await _signInManager.SignInAsync(user, true);

                // return a success response to the client
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet("isloggedin")]
        public IActionResult IsLoggedIn()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return Ok(new { IsLoggedIn = true });
            }
            else
            {
                return Ok(new { IsLoggedIn = false });
            }
        }

        [HttpGet("resource")]
        [Authorize]
        public IActionResult GetProtectedResource()
        {
            return Ok("This is a protected resource");
        }

        #pragma warning disable CS8618
        
        public class LoginModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class RegisterModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
