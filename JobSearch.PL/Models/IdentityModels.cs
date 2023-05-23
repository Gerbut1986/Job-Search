namespace JobSearch.PL.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal enum Role { Admin, Employer, RegisterUser, Guest, Unknown };

    public class ApplicationUser : IdentityUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime DateRegister { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime DateBirthday { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public string Experience { get; set; }
        public string Skills { get; set; }
        public string City { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}