using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyContext>();

//Use the Login entity for SignInManager
// builder.Services.AddIdentity<Login, MyRole>()
//     .AddEntityFrameworkStores<MyContext>()
//     .AddDefaultTokenProviders();

builder.Services.AddScoped<SignInManager<IdentityUser>, SignInManager<IdentityUser>>();

// register the custom user store as a transient service
// builder.Services.AddTransient<IUserStore<IdentityUser>, MyUserStore>();

// get an instance of IdentityBuilder with IdentityUser as the user entity class
var identityBuilder = builder.Services.AddIdentityCore<IdentityUser>();

// register the custom user store as an implementation of the IUserStore<IdentityUser> interface
identityBuilder.AddUserStore<MyUserStore>();



// builder.Services.ConfigureApplicationCookie(options =>
// {
//     options.LoginPath = "api/Auth/Login";
// });

// register the IHttpContextAccessor service as a singleton
builder.Services.AddHttpContextAccessor();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "MyCookie";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.SlidingExpiration = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomAuthorizationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
