using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitCoupon.DAL.Models;
using System.Data.Entity;

namespace BitCoupon.API.Models
{

   

    public class UserProfileViewModel
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarUrl { get; set; }

        public string Location { get; set; }

        public string Address { get; set; }

        public string Role { get; set; }

        public string Id { get; set; }

        public int NumberOfComments { get; set; }

        public List<Coupon> AllCoupons { get; set; }

        public List<Coupon> SellerCoupons { get; set; }

        public List<Comment> Comments { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public List<PayPalRefund> Refunds { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();
    


        public UserProfileViewModel(ApplicationUser user)
        {
           if (user != null)
            {
                FirstName = user.FirstName;
                SellerCoupons = db.Coupons.Where(x => x.ApplicationUserId == user.Id).ToList();
                Email = user.Email;
                FirstName = user.FirstName;
                LastName = user.LastName;
                Address = user.Address;
                Location = user.Location;
                Role = user.Role;
                Id = user.Id;
                AllCoupons = db.Coupons.OrderByDescending(x => x.CouponId).Take(2).ToList();
                AvatarUrl = user.AvatarUrl;
                Refunds = db.Refunds.Where(x => x.ApplicationUserId == user.Id).ToList();
                
                Comments = db.Comments.Where(x => x.ApplicationUserId == user.Id).ToList();
                NumberOfComments = Comments.Count;
                PhoneNumber = user.PhoneNumber;
                Gender = user.Gender;
            }

        }
    }
}