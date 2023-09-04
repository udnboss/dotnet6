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
        private readonly UserManager<Login> _userManager;
        private readonly SignInManager<Login> _signInManager;

        public AuthController(MyContext context,UserManager<Login> userManager, SignInManager<Login> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (result.Succeeded)
            {
                var login = await _userManager.FindByNameAsync(model.UserName);
                var user = _context.Users.First(u => u.LoginId == login.Id);
                
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

                await _userManager.AddClaimsAsync(login, claims);

                // await _userManager.AddClaimAsync(user, new Claim("Permission", AppPermission.AccountRead.GetCode()));
                
                return Ok();
            }
            else
            {
                return BadRequest("Invalid username or password");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var login = new Login { Id = Guid.NewGuid(), UserName = model.UserName };
            var result = await _userManager.CreateAsync(login, model.Password);

            if (result.Succeeded)
            {
                
                await _signInManager.SignInAsync(login, false);

                //create a user for the login
                var user = new User { Id = Guid.NewGuid(), Email = login.UserName, LoginId = login.Id, Name = login.UserName };
                _context.Users.Add(user);

                var adminRoleCode = AppRole.Admin.GetCode();
                var adminRole = _context.Roles.First(x => x.Code == adminRoleCode);

                //grant admin role
                var userRole = new UserRole { Id = Guid.NewGuid(), RoleId = adminRole.Id, UserId = user.Id };
                _context.UserRoles.Add(userRole);

                _context.SaveChanges();

                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
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
