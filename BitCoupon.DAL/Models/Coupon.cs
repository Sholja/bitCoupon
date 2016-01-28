using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.DAL.Models
{
    /// <summary>
    /// Represent coupon
    /// </summary>
	public class Coupon
	{
        public int CouponId { get; set; }

        [Required]
        [DisplayName("* Coupon")]
        [MaxLength(39)]
        public string Name { get; set; }

        [Range(1, 9999.99)]
        [DisplayName("* Price")]
        public double Price { get; set; }

        [Range(1, 9999.99)]
        [DisplayName("* New Price")]
        public double NewPrice { get; set; }

        [MinLength(10)]
        [MaxLength(62)]
        [Required]
        [DisplayName("* Description of Coupon on Main Page")]
        public string DescriptionOnCoupon { get; set; }

        [MinLength(100)]
        [MaxLength(500)]
        [Required]
        [DisplayName("* Detail Description of Coupon")]
        public string DescriptionOnSellerPage { get; set; }

        [Required]
         [BitCoupon.DAL.validators.ValidateDate]
        [DisplayName("* Date of Coupon Expiration")]
        public DateTime ExpirationTime { get; set; }

        [Required]
        [Range(0,1000)]
        [DisplayName("* Number of coupon purchases that satisfies your offer")]
        public int NuberOfCouponsToOfferManaged { get; set; }


        [Required]
        [Range(1, 1000)]
        [DisplayName("* Minimum Coupon Purchase per Buyer")]
        public int RequiredNumberOfCoupons { get; set; }

        [Required]
        [Range(0, 1000)]
        [DisplayName("* Total Number of Coupons")]
        public int TotalNumberOfCoupons { get; set; }

        [DisplayName("Company Web Site (Optional)")]
        public string SellerUrl { get; set; }

        [DisplayName("* Coupon Picture")]
        public string PictureUrl { get; set; }

        [DisplayName("* Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
      
        public List<int> ImageId { get; set; }
        public virtual List<Image> Images { get; set; }

        public List<int> GoogleApiId { get; set; }
        public virtual List<GoogleApi> Locations { get; set; }

        public List<int> CommentId { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public int Purchase { get; set; }

        public bool Acitve { get; set; }

        public int Priority { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsEdited { get; set; }

        public bool Approved { get; set; }

        public List<int> QuestionId { get; set; }
        public virtual List<Question> Questions { get; set; }
    }
}
