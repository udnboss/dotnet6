using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


public class MyContext : DbContext
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public DbSet<Login> Logins { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }
    

    public string DbPath { get; }

    public MyContext()
    {
        var path = "./Data";
        DbPath = Path.Join(path, "db.sqlite3");
    }

    #pragma warning restore CS8618

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}"); 

    //For Sqlite, convert Guid to string and vice versa
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var guidToStringConverter = new ValueConverter<Guid, string>(
            guid => guid.ToString(),
            str => Guid.Parse(str));

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(Guid))
                {
                    property.SetValueConverter(guidToStringConverter);
                }
                else if (property.ClrType == typeof(string))
                {
                    property.SetCollation("NOCASE");
                }
            }
        }
    }
}

public enum AppPermission
{
    LoginRead,
    LoginCreate,
    LoginUpdate,
    LoginDelete,
    LoginExecute,
    UserRead,
    UserCreate,
    UserUpdate,
    UserDelete,
    UserExecute,
    RoleRead,
    RoleCreate,
    RoleUpdate,
    RoleDelete,
    RoleExecute,
    UserRoleRead,
    UserRoleCreate,
    UserRoleUpdate,
    UserRoleDelete,
    UserRoleExecute,
    PermissionRead,
    PermissionCreate,
    PermissionUpdate,
    PermissionDelete,
    PermissionExecute,
    RolePermissionRead,
    RolePermissionCreate,
    RolePermissionUpdate,
    RolePermissionDelete,
    RolePermissionExecute,
    AccountRead,
    AccountCreate,
    AccountUpdate,
    AccountDelete,
    AccountExecute,
    CategoryRead,
    CategoryCreate,
    CategoryUpdate,
    CategoryDelete,
    CategoryExecute,
    CompanyRead,
    CompanyCreate,
    CompanyUpdate,
    CompanyDelete,
    CompanyExecute,
    CurrencyRead,
    CurrencyCreate,
    CurrencyUpdate,
    CurrencyDelete,
    CurrencyExecute,
    CustomerRead,
    CustomerCreate,
    CustomerUpdate,
    CustomerDelete,
    CustomerExecute,
    ItemRead,
    ItemCreate,
    ItemUpdate,
    ItemDelete,
    ItemExecute,
    SaleRead,
    SaleCreate,
    SaleUpdate,
    SaleDelete,
    SaleExecute,
    SaleItemRead,
    SaleItemCreate,
    SaleItemUpdate,
    SaleItemDelete,
    SaleItemExecute
}

public static class AppPermissionExtensions
{
    public static string GetCode(this AppPermission permission)
    {
        switch (permission)
        {    
            case AppPermission.LoginRead: return "ENTITY:LOGIN:READ";
            case AppPermission.LoginCreate: return "ENTITY:LOGIN:CREATE";
            case AppPermission.LoginUpdate: return "ENTITY:LOGIN:UPDATE";
            case AppPermission.LoginDelete: return "ENTITY:LOGIN:DELETE";
            case AppPermission.LoginExecute: return "ENTITY:LOGIN:EXECUTE";
            case AppPermission.UserRead: return "ENTITY:USER:READ";
            case AppPermission.UserCreate: return "ENTITY:USER:CREATE";
            case AppPermission.UserUpdate: return "ENTITY:USER:UPDATE";
            case AppPermission.UserDelete: return "ENTITY:USER:DELETE";
            case AppPermission.UserExecute: return "ENTITY:USER:EXECUTE";
            case AppPermission.RoleRead: return "ENTITY:ROLE:READ";
            case AppPermission.RoleCreate: return "ENTITY:ROLE:CREATE";
            case AppPermission.RoleUpdate: return "ENTITY:ROLE:UPDATE";
            case AppPermission.RoleDelete: return "ENTITY:ROLE:DELETE";
            case AppPermission.RoleExecute: return "ENTITY:ROLE:EXECUTE";
            case AppPermission.UserRoleRead: return "ENTITY:USERROLE:READ";
            case AppPermission.UserRoleCreate: return "ENTITY:USERROLE:CREATE";
            case AppPermission.UserRoleUpdate: return "ENTITY:USERROLE:UPDATE";
            case AppPermission.UserRoleDelete: return "ENTITY:USERROLE:DELETE";
            case AppPermission.UserRoleExecute: return "ENTITY:USERROLE:EXECUTE";
            case AppPermission.PermissionRead: return "ENTITY:PERMISSION:READ";
            case AppPermission.PermissionCreate: return "ENTITY:PERMISSION:CREATE";
            case AppPermission.PermissionUpdate: return "ENTITY:PERMISSION:UPDATE";
            case AppPermission.PermissionDelete: return "ENTITY:PERMISSION:DELETE";
            case AppPermission.PermissionExecute: return "ENTITY:PERMISSION:EXECUTE";
            case AppPermission.RolePermissionRead: return "ENTITY:ROLEPERMISSION:READ";
            case AppPermission.RolePermissionCreate: return "ENTITY:ROLEPERMISSION:CREATE";
            case AppPermission.RolePermissionUpdate: return "ENTITY:ROLEPERMISSION:UPDATE";
            case AppPermission.RolePermissionDelete: return "ENTITY:ROLEPERMISSION:DELETE";
            case AppPermission.RolePermissionExecute: return "ENTITY:ROLEPERMISSION:EXECUTE";
            case AppPermission.AccountRead: return "ENTITY:ACCOUNT:READ";
            case AppPermission.AccountCreate: return "ENTITY:ACCOUNT:CREATE";
            case AppPermission.AccountUpdate: return "ENTITY:ACCOUNT:UPDATE";
            case AppPermission.AccountDelete: return "ENTITY:ACCOUNT:DELETE";
            case AppPermission.AccountExecute: return "ENTITY:ACCOUNT:EXECUTE";
            case AppPermission.CategoryRead: return "ENTITY:CATEGORY:READ";
            case AppPermission.CategoryCreate: return "ENTITY:CATEGORY:CREATE";
            case AppPermission.CategoryUpdate: return "ENTITY:CATEGORY:UPDATE";
            case AppPermission.CategoryDelete: return "ENTITY:CATEGORY:DELETE";
            case AppPermission.CategoryExecute: return "ENTITY:CATEGORY:EXECUTE";
            case AppPermission.CompanyRead: return "ENTITY:COMPANY:READ";
            case AppPermission.CompanyCreate: return "ENTITY:COMPANY:CREATE";
            case AppPermission.CompanyUpdate: return "ENTITY:COMPANY:UPDATE";
            case AppPermission.CompanyDelete: return "ENTITY:COMPANY:DELETE";
            case AppPermission.CompanyExecute: return "ENTITY:COMPANY:EXECUTE";
            case AppPermission.CurrencyRead: return "ENTITY:CURRENCY:READ";
            case AppPermission.CurrencyCreate: return "ENTITY:CURRENCY:CREATE";
            case AppPermission.CurrencyUpdate: return "ENTITY:CURRENCY:UPDATE";
            case AppPermission.CurrencyDelete: return "ENTITY:CURRENCY:DELETE";
            case AppPermission.CurrencyExecute: return "ENTITY:CURRENCY:EXECUTE";
            case AppPermission.CustomerRead: return "ENTITY:CUSTOMER:READ";
            case AppPermission.CustomerCreate: return "ENTITY:CUSTOMER:CREATE";
            case AppPermission.CustomerUpdate: return "ENTITY:CUSTOMER:UPDATE";
            case AppPermission.CustomerDelete: return "ENTITY:CUSTOMER:DELETE";
            case AppPermission.CustomerExecute: return "ENTITY:CUSTOMER:EXECUTE";
            case AppPermission.ItemRead: return "ENTITY:ITEM:READ";
            case AppPermission.ItemCreate: return "ENTITY:ITEM:CREATE";
            case AppPermission.ItemUpdate: return "ENTITY:ITEM:UPDATE";
            case AppPermission.ItemDelete: return "ENTITY:ITEM:DELETE";
            case AppPermission.ItemExecute: return "ENTITY:ITEM:EXECUTE";
            case AppPermission.SaleRead: return "ENTITY:SALE:READ";
            case AppPermission.SaleCreate: return "ENTITY:SALE:CREATE";
            case AppPermission.SaleUpdate: return "ENTITY:SALE:UPDATE";
            case AppPermission.SaleDelete: return "ENTITY:SALE:DELETE";
            case AppPermission.SaleExecute: return "ENTITY:SALE:EXECUTE";
            case AppPermission.SaleItemRead: return "ENTITY:SALEITEM:READ";
            case AppPermission.SaleItemCreate: return "ENTITY:SALEITEM:CREATE";
            case AppPermission.SaleItemUpdate: return "ENTITY:SALEITEM:UPDATE";
            case AppPermission.SaleItemDelete: return "ENTITY:SALEITEM:DELETE";
            case AppPermission.SaleItemExecute: return "ENTITY:SALEITEM:EXECUTE";            
            default:
                throw new ArgumentOutOfRangeException(nameof(permission), permission, null);
        }
    }
}

public enum AppRole
{
    Admin,
    Contribute,
    View
}

public static class AppRoleExtensions
{
    public static string GetCode(this AppRole appRole)
    {
        switch (appRole)
        {    
            case AppRole.Admin: return "ADMIN";         
            case AppRole.Contribute: return "CONTRIBUTE";         
            case AppRole.View: return "VIEW";         
            default:
                throw new ArgumentOutOfRangeException(nameof(appRole), appRole, null);
        }
    }
}

public class MyRole : IdentityRole<Guid> 
{

}