using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace BitCoupon.DAL.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Location { get; set; }


        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Address { get; set; }

        public bool FirstSignin { get; set; }

        public string Role { get; set; }

        public string Gender { get; set; }

        public bool IsCreatedBySalesman { get; set; }

        public string AvatarUrl { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
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

		public System.Data.Entity.DbSet<BitCoupon.DAL.Models.Category> Categories { get; set; }

		public System.Data.Entity.DbSet<BitCoupon.DAL.Models.Coupon> Coupons { get; set; }

        public System.Data.Entity.DbSet<BitCoupon.DAL.Models.Image> Images { get; set; }

        public System.Data.Entity.DbSet<BitCoupon.DAL.Models.GoogleApi> GoogleApis { get; set; }


        public System.Data.Entity.DbSet<BitCoupon.DAL.Models.Report> Reports { get; set; }

        public System.Data.Entity.DbSet<BitCoupon.DAL.Models.Message> Messages { get; set; }

        public System.Data.Entity.DbSet<BitCoupon.DAL.Models.Buy> Buys { get; set; }

        public System.Data.Entity.DbSet<BitCoupon.DAL.Models.Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<BitCoupon.DAL.Models.FeedBack> FeedBacks { get; set; }

        public System.Data.Entity.DbSet<BitCoupon.DAL.Models.Cart> Carts { get; set; }

        public System.Data.Entity.DbSet<BitCoupon.DAL.Models.Item> Items { get; set; }

        public System.Data.Entity.DbSet<BitCoupon.DAL.Models.PayPalRefund> Refunds { get; set; }

        public System.Data.Entity.DbSet<BitCoupon.DAL.Models.Mail> Mails { get; set; }

        public System.Data.Entity.DbSet<BitCoupon.DAL.Models.Avatar> Avatars { get; set; }

        public System.Data.Entity.DbSet<BitCoupon.DAL.Models.Question> Questions { get; set; }
    }
}